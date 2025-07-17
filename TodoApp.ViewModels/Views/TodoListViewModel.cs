using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Dispatching;
using TodoApp.Core;
using TodoApp.Core.Services;
using TodoApp.ViewModels.Factories;
using TodoApp.ViewModels.Localization;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoListViewModel : BaseViewModel, IRecipient<TodoItemCreatedMessage>, IRecipient<TodoItemUpdatedMessage>, IDisposable
{
    private readonly ITodoItemsService _todoItemsService;
    private readonly ITodoListItemSummaryViewModelFactory _todoItemSummaryFactory;
    private readonly ViewModelContext _context;

    [ObservableProperty]
    public partial ObservableCollection<TodoListItemSummaryViewModel> TodoItems { get; set; }

    public int CompletedCount => TodoItems.Count(x => x.IsCompleted);

    public int TotalCount => TodoItems.Count;

    public string CompletedSummary => $"{_context.Localization.GetString(Strings.Completed)}: {CompletedCount}/{TotalCount}";

    public TodoListViewModel(
        ViewModelContext context,
        ITodoItemsService todoItemsService,
        ITodoListItemSummaryViewModelFactory todoItemSummaryFactory)
    {
        _context = context;
        _todoItemsService = todoItemsService;
        _todoItemSummaryFactory = todoItemSummaryFactory;
        TodoItems = [];

        context.Messenger.Register<TodoItemCreatedMessage>(this);
        context.Messenger.Register<TodoItemUpdatedMessage>(this);

        TodoItems.CollectionChanged += TodoItems_CollectionChanged;
    }

    private void TodoItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CompletedCount));
        OnPropertyChanged(nameof(TotalCount));
        OnPropertyChanged(nameof(CompletedSummary));
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

        var localizedString = _context.Localization.GetString(Strings.TodoAdded);

        await _context.Snackbar
            .ShowAsync(localizedString)
            .ConfigureAwait(false);
    }

    async void IRecipient<TodoItemUpdatedMessage>.Receive(TodoItemUpdatedMessage message)
    {
        var todoItemVm = TodoItems.First(x => x.TodoItem.Id == message.Value.Id);

        todoItemVm.Title = message.Value.Title;
        todoItemVm.Deadline = message.Value.Deadline;
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

        if (!message.OnlyCompleted)
        {
            var localizedString = _context.Localization.GetString(Strings.TodoUpdated);

            await _context.Snackbar
                .ShowAsync(localizedString)
                .ConfigureAwait(false);
        }
    }

    public async Task LoadItemsAsync()
    {
        var items = await _todoItemsService
            .GetAsync()
            .ConfigureAwait(false);

        var orderedItems = items
            .OrderBy(x => x.Completed)
            .ThenBy(x => !x.Important)
            .ThenBy(x => x.Deadline)
            .ToList();

        foreach (var item in orderedItems)
        {
            var summaryViewModel = _todoItemSummaryFactory.Create(item);

            await _context.Dispatcher
                .DispatchAsync(() => TodoItems.Add(summaryViewModel))
                .ConfigureAwait(false);
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
            .ThenBy(x => x.Deadline);
    }

    [RelayCommand]
    private async Task EditAsync(TodoListItemSummaryViewModel todoListItemSummaryViewModel, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "TodoItem", todoListItemSummaryViewModel.TodoItem }
        };

        await _context.Navigation
            .NavigateToAsync(Routes.EditTodoItem, parameters, cancellationToken)
            .ConfigureAwait(false);
    }

    public void Dispose()
    {
        TodoItems.CollectionChanged -= TodoItems_CollectionChanged;
    }
}