using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Networking;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Pages;

public sealed partial class NewTodoViewModel : BaseViewModel
{
    private readonly IConnectivity _connectivity;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;
    private readonly ITodoItemsService _todoItemsService;
    
    public TodoItemFormViewModel FormViewModel { get; }
    
    public NewTodoViewModel(
        ITodoItemsService todoItemsService,
        IMessenger messenger,
        IConnectivity connectivity,
        INavigation navigation,
        TodoItemFormViewModel formViewModel)
    {
        _todoItemsService = todoItemsService;
        _messenger = messenger;
        _connectivity = connectivity;
        _navigation = navigation;
        FormViewModel = formViewModel;
    }
    
    [RelayCommand]
    private async Task AddAsync(CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem
        {
            Title = FormViewModel.Title,
            Details = FormViewModel.Details,
            Category = FormViewModel.Category,
            DueDate = FormViewModel.DueDate,
            CreatedDate = DateTimeOffset.Now,
            Important = FormViewModel.Important,
            Completed = FormViewModel.Completed,
        };
    
        var id = await _todoItemsService
            .AddAsync(todoItem, cancellationToken)
            .ConfigureAwait(false);
    
        todoItem.Id = id;
    
        _messenger.Send(new TodoItemCreatedMessage(todoItem));
        await _navigation.GoBackAsync(cancellationToken);
    }
}