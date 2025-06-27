using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TodoApp.Core;
using TodoApp.ViewModels.Messages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.ViewModels;

public sealed partial class TodoItemDetailsViewModel : BaseViewModel
{
    private readonly IMessenger _messenger;
    private readonly INavigation _navigation;

    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    private string _category = string.Empty;

    [ObservableProperty]
    private bool _completed;

    [ObservableProperty]
    private string _details = string.Empty;

    [ObservableProperty]
    private DateTimeOffset? _dueDate;

    [ObservableProperty]
    private bool _important;

    [ObservableProperty]
    private string _title = string.Empty;

    /// <summary>
    ///     Passed through IQueryAttributable
    /// </summary>
    [ObservableProperty]
    private TodoItem _todoItem = null!;

    public TodoItemDetailsViewModel(ITodoItemsService todoItemsService, INavigation navigation, IMessenger messenger)
    {
        _todoItemsService = todoItemsService;
        _navigation = navigation;
        _messenger = messenger;
    }

    private DateTimeOffset CreatedDate { get; set; }

    // this is here and not in constructor cause of navigation
    partial void OnTodoItemChanged(TodoItem value)
    {
        Title = value.Title;
        Details = value.Details;
        Category = value.Category;
        CreatedDate = value.CreatedDate;
        DueDate = value.DueDate;
        Important = value.Important;
        Completed = value.Completed;
    }

    [RelayCommand]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        TodoItem.Title = Title;
        TodoItem.Details = Details;
        TodoItem.Category = Category;
        TodoItem.DueDate = DueDate;
        TodoItem.Important = Important;
        TodoItem.Completed = Completed;

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