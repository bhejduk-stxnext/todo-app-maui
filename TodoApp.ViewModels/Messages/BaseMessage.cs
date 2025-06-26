using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TodoApp.ViewModels.Messages;

public class BaseMessage : ValueChangedMessage<string>
{
    public BaseMessage(string value)
        : base(value) { }
}