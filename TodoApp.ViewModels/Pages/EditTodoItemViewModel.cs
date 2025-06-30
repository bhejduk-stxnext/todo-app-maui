using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Pages;

public sealed partial class EditTodoItemViewModel : BaseViewModel
{
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;

    private readonly ITodoItemsService _todoItemsService;

    /// <summary>
    ///     Passed through IQueryAttributable
    /// </summary>
    [ObservableProperty]
    private TodoItem _todoItem = null!;

    public EditTodoItemViewModel(
        ITodoItemsService todoItemsService,
        INavigation navigation,
        IMessenger messenger,
        TodoItemFormViewModel formViewModel)
    {
        _todoItemsService = todoItemsService;
        _navigation = navigation;
        _messenger = messenger;
        FormViewModel = formViewModel;
    }

    public TodoItemFormViewModel FormViewModel { get; }

    private DateTimeOffset CreatedDate { get; set; }

    // this is here and not in constructor cause of navigation
    partial void OnTodoItemChanged(TodoItem value)
    {
        FormViewModel.Title = value.Title;
        FormViewModel.Details = value.Details;
        FormViewModel.Category = value.Category;
        FormViewModel.CreatedDate = value.CreatedDate;
        FormViewModel.DueDate = value.DueDate;
        FormViewModel.Important = value.Important;
        FormViewModel.Completed = value.Completed;
    }

    private bool CanSave()
    {
        return FormViewModel.CanSave();
    }

    [RelayCommand(CanExecute = nameof(CanSave))]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        TodoItem.Title = FormViewModel.Title;
        TodoItem.Details = FormViewModel.Details;
        TodoItem.Category = FormViewModel.Category;
        TodoItem.DueDate = FormViewModel.DueDate;
        TodoItem.Important = FormViewModel.Important;
        TodoItem.Completed = FormViewModel.Completed;

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