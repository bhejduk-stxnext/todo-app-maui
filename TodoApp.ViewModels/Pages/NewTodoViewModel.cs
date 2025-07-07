using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Networking;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Models;
using TodoApp.ViewModels.Navigation;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Pages;

public sealed partial class NewTodoViewModel : BaseViewModel
{
    private readonly ViewModelContext _context;
    private readonly ITodoItemsService _todoItemsService;

    public TodoItemFormViewModel FormViewModel { get; }

    public NewTodoViewModel(
        ViewModelContext context,
        ITodoItemsService todoItemsService,
        TodoItemFormViewModel formViewModel)
    {
        _todoItemsService = todoItemsService;
        FormViewModel = formViewModel;
        _context = context;
    }

    [RelayCommand]
    private async Task AddAsync(CancellationToken cancellationToken)
    {
        var todoItem = new TodoItem
        {
            Title = FormViewModel.Title,
            Details = FormViewModel.Details,
            Category = FormViewModel.SelectedCategory == NoneCategory.Instance
                ? null
                : FormViewModel.SelectedCategory,
            Deadline = FormViewModel.Deadline,
            CreatedDate = DateTimeOffset.Now,
            Important = FormViewModel.Important,
            Completed = FormViewModel.Completed
        };

        var id = await _todoItemsService
            .AddAsync(todoItem, cancellationToken)
            .ConfigureAwait(false);

        todoItem.Id = id;

        _context.Messenger.Send(new TodoItemCreatedMessage(todoItem));

        await _context.Navigation
            .GoBackAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}