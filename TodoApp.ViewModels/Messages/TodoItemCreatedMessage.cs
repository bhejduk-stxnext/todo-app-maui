using CommunityToolkit.Mvvm.Messaging.Messages;
using TodoApp.Core;

namespace TodoApp.ViewModels.Messages;

public sealed class TodoItemCreatedMessage : ValueChangedMessage<TodoItem>
{
    public TodoItemCreatedMessage(TodoItem value)
        : base(value) { }
}