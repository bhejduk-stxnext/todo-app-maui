using CommunityToolkit.Mvvm.ComponentModel;
using TodoApp.Core;

namespace TodoApp.ViewModels;

public sealed partial class TodoListItemSummaryViewModel : BaseViewModel
{
    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isCompleted;

    public bool DeadlineExceeded { get; }
    
    public TodoItem TodoItem { get; }
    
    public TodoListItemSummaryViewModel(TodoItem todoItem, ITodoItemsService todoItemsService, TimeProvider timeProvider)
    {
        TodoItem = todoItem;
        _todoItemsService = todoItemsService;
        IsCompleted = todoItem.Completed;
        DeadlineExceeded = todoItem.DueDate > timeProvider.GetUtcNow();
    }

    partial void OnIsCompletedChanged(bool value)
    {
        // Fire-and-forget update
        _ = UpdateCompletionAsync(value);
    }

    private async Task UpdateCompletionAsync(bool value)
    {
        try
        {
            TodoItem.Completed = value;

            await _todoItemsService.UpdateAsync(TodoItem);
        }
        catch (Exception)
        {
            IsCompleted = !value;
        }
    }
}