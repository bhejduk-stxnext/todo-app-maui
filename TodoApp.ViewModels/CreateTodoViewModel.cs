using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Networking;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels;

public sealed partial class CreateTodoViewModel : BaseViewModel
{
    private readonly IConnectivity _connectivity;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;
    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    private string _details = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _dueDate;

    [ObservableProperty]
    private bool _important;

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;

    public CreateTodoViewModel(
        ITodoItemsService todoItemsService,
        IMessenger messenger,
        IConnectivity connectivity,
        INavigation navigation)
    {
        _todoItemsService = todoItemsService;
        _messenger = messenger;
        _connectivity = connectivity;
        _navigation = navigation;
    }

    [RelayCommand(CanExecute = nameof(IsBusy))]
    private async Task SaveAsync(CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem
        {
            Title = Title,
            Details = Details,
            DueDate = DueDate,
            CreatedDate = DateTimeOffset.Now
        };

        var id = await _todoItemsService
            .AddAsync(todoItem, cancellationToken)
            .ConfigureAwait(false);

        todoItem.Id = id;

        _messenger.Send(new TodoItemCreatedMessage(todoItem));
        await _navigation.GoBackAsync(cancellationToken);
    }

    [RelayCommand]
    private async Task CancelAsync(CancellationToken cancellationToken)
    {
        await _navigation.GoBackAsync(cancellationToken);
    }
}