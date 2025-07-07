using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Models;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels.Views;

public sealed partial class TodoItemDetailsViewModel : BaseViewModel
{
    private readonly TodoItemFormViewModel _formViewModel;
    private readonly ViewModelContext _context;
    private readonly ITodoItemsService _todoItemsService;

    /// <summary>
    ///     Passed through IQueryAttributable
    /// </summary>
    [ObservableProperty]
    private TodoItem _todoItem = null!;

    public TodoItemDetailsViewModel(
        ITodoItemsService todoItemsService,
        ViewModelContext context,
        TodoItemFormViewModel formViewModel)
    {
        _todoItemsService = todoItemsService;
        _context = context;
        _formViewModel = formViewModel;
    }

    private DateTimeOffset CreatedDate { get; set; }

    // this is here and not in constructor cause of navigation
    partial void OnTodoItemChanged(TodoItem value)
    {
        _formViewModel.Title = value.Title;
        _formViewModel.Details = value.Details;
        _formViewModel.SelectedCategory = value.Category ?? NoneCategory.Instance;
        _formViewModel.CreatedDate = value.CreatedDate.LocalDateTime;
        _formViewModel.Deadline = value.Deadline?.LocalDateTime;
        _formViewModel.Important = value.Important;
        _formViewModel.Completed = value.Completed;
    }

    [RelayCommand]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        TodoItem.Title = _formViewModel.Title;
        TodoItem.Details = _formViewModel.Details;

        TodoItem.Category = _formViewModel.SelectedCategory == NoneCategory.Instance
            ? null
            : _formViewModel.SelectedCategory;

        TodoItem.Deadline = _formViewModel.Deadline;
        TodoItem.Important = _formViewModel.Important;
        TodoItem.Completed = _formViewModel.Completed;

        try
        {
            await _todoItemsService
                .UpdateAsync(TodoItem, cancellationToken)
                .ConfigureAwait(false);

            var message = new TodoItemUpdatedMessage(TodoItem);

            _context.Messenger.Send(message);

            await _context.Navigation
                .GoBackAsync(cancellationToken)
                .ConfigureAwait(false);
        }
        catch
        {
            // ignored
        }
    }
}