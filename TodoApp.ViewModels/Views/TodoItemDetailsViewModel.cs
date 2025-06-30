using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoItemDetailsViewModel : BaseViewModel
{
    private readonly TodoItemFormViewModel _formViewModel;
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;

    private readonly ITodoItemsService _todoItemsService;

    /// <summary>
    ///     Passed through IQueryAttributable
    /// </summary>
    [ObservableProperty]
    private TodoItem _todoItem = null!;

    public TodoItemDetailsViewModel(
        ITodoItemsService todoItemsService,
        INavigation navigation,
        IMessenger messenger,
        TodoItemFormViewModel formViewModel)
    {
        _todoItemsService = todoItemsService;
        _navigation = navigation;
        _messenger = messenger;
        _formViewModel = formViewModel;
    }

    private DateTimeOffset CreatedDate { get; set; }

    // this is here and not in constructor cause of navigation
    partial void OnTodoItemChanged(TodoItem value)
    {
        _formViewModel.Title = value.Title;
        _formViewModel.Details = value.Details;
        _formViewModel.Category = value.Category;
        _formViewModel.CreatedDate = value.CreatedDate;
        _formViewModel.DueDate = value.DueDate;
        _formViewModel.Important = value.Important;
        _formViewModel.Completed = value.Completed;
    }

    [RelayCommand]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        TodoItem.Title = _formViewModel.Title;
        TodoItem.Details = _formViewModel.Details;
        TodoItem.Category = _formViewModel.Category;
        TodoItem.DueDate = _formViewModel.DueDate;
        TodoItem.Important = _formViewModel.Important;
        TodoItem.Completed = _formViewModel.Completed;

        try
        {
            await _todoItemsService
                .UpdateAsync(TodoItem, cancellationToken)
                .ConfigureAwait(false);

            var message = new TodoItemUpdatedMessage(TodoItem);

            _messenger.Send(message);

            await _navigation
                .GoBackAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch
        {
            // ignored
        }
    }
}