using TodoApp.Core;

namespace TodoApp.ViewModels.Factories;

public class TodoListItemSummaryViewModelFactory : ITodoListItemSummaryViewModelFactory
{
    private readonly ITodoItemsService _todoItemsService;
    private readonly TimeProvider _timeProvider;

    public TodoListItemSummaryViewModelFactory(ITodoItemsService todoItemsService, TimeProvider timeProvider)
    {
        _todoItemsService = todoItemsService;
        _timeProvider = timeProvider;
    }

    public TodoListItemSummaryViewModel Create(TodoItem todoItem)
    {
        return new TodoListItemSummaryViewModel(todoItem, _todoItemsService, _timeProvider);
    }
}