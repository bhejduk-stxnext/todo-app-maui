using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Dispatching;
using TodoApp.Core;
using TodoApp.ViewModels.Factories;
using TodoApp.ViewModels.Navigation;
using TodoApp.ViewModels.ObjectModel;

namespace TodoApp.ViewModels;

public partial class TodoListViewModel : BaseViewModel
{
    private readonly IDispatcher _dispatcher;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;
    private readonly ITodoItemsService _todoItemsService;
    private readonly ITodoListItemSummaryViewModelFactory _todoItemSummaryFactory;

    [ObservableProperty]
    private SortableObservableCollection<TodoListItemSummaryViewModel> _todoItems;

    public TodoListViewModel(
        ITodoItemsService todoItemsService,
        IDispatcher dispatcher,
        IMessenger messenger,
        ITodoListItemSummaryViewModelFactory todoItemSummaryFactory,
        INavigation navigation)
    {
        _todoItemsService = todoItemsService;
        _dispatcher = dispatcher;
        _messenger = messenger;
        _todoItemSummaryFactory = todoItemSummaryFactory;
        _navigation = navigation;
        _todoItems = [];
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

            await _dispatcher.DispatchAsync(() => TodoItems.Add(summaryViewModel));
        }
    }

    public IOrderedEnumerable<TodoListItemSummaryViewModel> GetSorted()
    {
        return TodoItems
            .OrderBy(x => x.IsCompleted)
            .ThenBy(x => !x.Important)
            .ThenBy(x => x.DueDate);
    }

    public Task RefreshItemsAsync()
    {
        Func<SortableObservableCollection<TodoListItemSummaryViewModel>, IOrderedEnumerable<TodoListItemSummaryViewModel>> orderBy = soc => soc
            .OrderBy(vm => vm.IsCompleted)
            .ThenBy(vm => !vm.Important);

        TodoItems.Sort(orderBy);

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task EditAsync(TodoListItemSummaryViewModel todoListItemSummaryViewModel, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "TodoItem", todoListItemSummaryViewModel.TodoItem }
        };

        await _navigation
            .NavigateToAsync(Routes.TodoItemDetails, parameters, cancellationToken)
            .ConfigureAwait(false);
    }
}