using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Factories;

public class TodoListItemSummaryViewModelFactory : ITodoListItemSummaryViewModelFactory
{
    private readonly ViewModelContext _context;
    private readonly ITodoItemsService _todoItemsService;

    public TodoListItemSummaryViewModelFactory(ITodoItemsService todoItemsService, ViewModelContext context)
    {
        _todoItemsService = todoItemsService;
        _context = context;
    }

    public TodoListItemSummaryViewModel Create(TodoItem todoItem)
    {
        return new TodoListItemSummaryViewModel(
            todoItem,
            _context,
            _todoItemsService);
    }
}