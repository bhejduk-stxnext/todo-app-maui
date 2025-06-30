using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoListItemSummaryViewModel : BaseViewModel
{
    private readonly IMessenger _messenger;
    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    private DateTimeOffset? _dueDate;

    [ObservableProperty]
    private bool _important;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private bool _isCompleted;

    [ObservableProperty]
    private string _title = string.Empty;

    public TodoListItemSummaryViewModel(
        TodoItem todoItem,
        ITodoItemsService todoItemsService,
        TimeProvider timeProvider,
        IMessenger messenger)
    {
        TodoItem = todoItem;
        _todoItemsService = todoItemsService;
        _messenger = messenger;
        Title = todoItem.Title;
        DueDate = todoItem.DueDate;
        Important = todoItem.Important;
        IsCompleted = todoItem.Completed;
        DeadlineExceeded = todoItem.DueDate > timeProvider.GetUtcNow();
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

            await _todoItemsService.UpdateAsync(TodoItem);

            var message = new TodoItemUpdatedMessage(TodoItem);
            _messenger.Send(message);
        }
        catch (Exception)
        {
            IsCompleted = !value;
        }
    }
}