using CommunityToolkit.Mvvm.Messaging.Messages;
using TodoApp.Core;

namespace TodoApp.ViewModels.Messages;

public sealed class TodoItemUpdatedMessage : ValueChangedMessage<TodoItem>
{
    public bool OnlyCompleted { get; set; }
    
    public TodoItemUpdatedMessage(TodoItem value, bool onlyCompleted = false)
        : base(value)
    {
        OnlyCompleted = onlyCompleted;
    }
}