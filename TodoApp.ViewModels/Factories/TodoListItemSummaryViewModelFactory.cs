using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Factories;

public class TodoListItemSummaryViewModelFactory : ITodoListItemSummaryViewModelFactory
{
    private readonly IMessenger _messenger;
    private readonly TimeProvider _timeProvider;
    private readonly ITodoItemsService _todoItemsService;

    public TodoListItemSummaryViewModelFactory(ITodoItemsService todoItemsService, TimeProvider timeProvider, IMessenger messenger)
    {
        _todoItemsService = todoItemsService;
        _timeProvider = timeProvider;
        _messenger = messenger;
    }

    public TodoListItemSummaryViewModel Create(TodoItem todoItem)
    {
        return new TodoListItemSummaryViewModel(
            todoItem,
            _todoItemsService,
            _timeProvider,
            _messenger);
    }
}