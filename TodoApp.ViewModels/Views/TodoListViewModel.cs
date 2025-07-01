using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Dispatching;
using TodoApp.Core;
using TodoApp.ViewModels.Factories;
using TodoApp.ViewModels.Localization;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoListViewModel : BaseViewModel, IRecipient<TodoItemCreatedMessage>, IRecipient<TodoItemUpdatedMessage>
{
    private readonly IDispatcher _dispatcher;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;
    private readonly ILocalization _localization;
    private readonly ITodoItemsService _todoItemsService;
    private readonly ITodoListItemSummaryViewModelFactory _todoItemSummaryFactory;
    private readonly ISnackbar _snackbar;

    [ObservableProperty]
    private ObservableCollection<TodoListItemSummaryViewModel> _todoItems;

    public TodoListViewModel(
        ITodoItemsService todoItemsService,
        IDispatcher dispatcher,
        IMessenger messenger,
        ITodoListItemSummaryViewModelFactory todoItemSummaryFactory,
        INavigation navigation,
        ISnackbar snackbar,
        ILocalization localization)
    {
        _todoItemsService = todoItemsService;
        _dispatcher = dispatcher;
        _messenger = messenger;
        _todoItemSummaryFactory = todoItemSummaryFactory;
        _navigation = navigation;
        _snackbar = snackbar;
        _localization = localization;
        _todoItems = [];

        _messenger.Register<TodoItemCreatedMessage>(this);
        _messenger.Register<TodoItemUpdatedMessage>(this);
    }

    async void IRecipient<TodoItemCreatedMessage>.Receive(TodoItemCreatedMessage message)
    {
        var newList = TodoItems.ToList();
        var todoItemViewModel = _todoItemSummaryFactory.Create(message.Value);
        newList.Add(todoItemViewModel);

        var sorted = GetSorted(newList)
            .ToList();

        var index = sorted.IndexOf(todoItemViewModel);

        TodoItems.Insert(index, todoItemViewModel);

        var localizedString = _localization.GetString(Strings.TodoAdded);
        await _snackbar.ShowAsync(localizedString).ConfigureAwait(false);
    }

    async void IRecipient<TodoItemUpdatedMessage>.Receive(TodoItemUpdatedMessage message)
    {
        var todoItemVm = TodoItems.First(x => x.TodoItem.Id == message.Value.Id);

        todoItemVm.Title = message.Value.Title;
        todoItemVm.DueDate = message.Value.DueDate;
        todoItemVm.Important = message.Value.Important;
        todoItemVm.IsCompleted = message.Value.Completed;

        var sorted = GetSorted()
            .ToList();

        var oldIndex = TodoItems.IndexOf(todoItemVm);
        var newIndex = sorted.IndexOf(todoItemVm);
#if WINDOWS
        // https://github.com/dotnet/maui/issues/17369#issuecomment-1830919911
        ViewModel.TodoItems.RemoveAt(oldIndex);
        ViewModel.TodoItems.Insert(newIndex, vm);
#else
        TodoItems.Move(oldIndex, newIndex);
#endif

        var localizedString = _localization.GetString(Strings.TodoUpdated);
        await _snackbar.ShowAsync(localizedString).ConfigureAwait(false);
    }

    public async Task LoadItemsAsync()
    {
        var items = await _todoItemsService
            .GetAsync()
            .ConfigureAwait(false);

        var orderedItems = items
            .OrderBy(x => x.Completed)
            .ThenBy(x => !x.Important)
            .ThenBy(x => x.DueDate)
            .ToList();

        foreach (var item in orderedItems)
        {
            var summaryViewModel = _todoItemSummaryFactory.Create(item);

            await _dispatcher.DispatchAsync(() => TodoItems.Add(summaryViewModel)).ConfigureAwait(false);
        }
    }

    public IOrderedEnumerable<TodoListItemSummaryViewModel> GetSorted()
    {
        return GetSorted(TodoItems);
    }

    private static IOrderedEnumerable<TodoListItemSummaryViewModel> GetSorted(IEnumerable<TodoListItemSummaryViewModel> items)
    {
        return items
            .OrderBy(x => x.IsCompleted)
            .ThenBy(x => !x.Important)
            .ThenBy(x => x.DueDate);
    }

    [RelayCommand]
    private async Task EditAsync(TodoListItemSummaryViewModel todoListItemSummaryViewModel, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "TodoItem", todoListItemSummaryViewModel.TodoItem }
        };

        await _navigation
            .NavigateToAsync(Routes.EditTodoItem, parameters, cancellationToken)
            .ConfigureAwait(false);
    }
}