using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoListItemSummaryViewModel : BaseViewModel
{
    private readonly ViewModelContext _context;
    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    public partial DateTimeOffset? Deadline { get; set; }

    [ObservableProperty]
    public partial bool Important { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsCompleted { get; set; }

    [ObservableProperty]
    public partial string Title { get; set; } = string.Empty;

    public TodoListItemSummaryViewModel(
        TodoItem todoItem,
        ViewModelContext context,
        ITodoItemsService todoItemsService)
    {
        TodoItem = todoItem;
        _todoItemsService = todoItemsService;
        _context = context;
        Title = todoItem.Title;
        Deadline = todoItem.Deadline;
        Important = todoItem.Important;
        IsCompleted = todoItem.Completed;
        DeadlineExceeded = todoItem.Deadline > _context.TimeProvider.GetUtcNow();
    }

    public bool DeadlineExceeded { get; }

    public TodoItem TodoItem { get; }

    partial void OnIsCompletedChanged(bool value)
    {
        // Fire-and-forget update
        _ = UpdateCompletionAsync(value);
    }

    private async Task UpdateCompletionAsync(bool value)
    {
        if (TodoItem.Completed == value)
            return;

        try
        {
            TodoItem.Completed = value;

            await _todoItemsService
                .UpdateAsync(TodoItem)
                .ConfigureAwait(false);

            var message = new TodoItemUpdatedMessage(TodoItem, true);
            _context.Messenger.Send(message);
        }
        catch (Exception)
        {
            IsCompleted = !value;
        }
    }
}