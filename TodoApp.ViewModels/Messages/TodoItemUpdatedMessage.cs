using CommunityToolkit.Mvvm.Messaging.Messages;
using TodoApp.Core;

namespace TodoApp.ViewModels.Messages;

public sealed class TodoItemUpdatedMessage : ValueChangedMessage<TodoItem>
{
    public TodoItemUpdatedMessage(TodoItem value)
        : base(value) { }
}