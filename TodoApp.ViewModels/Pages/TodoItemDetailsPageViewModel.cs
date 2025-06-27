namespace TodoApp.ViewModels.Pages;

public class TodoItemDetailsPageViewModel : BaseViewModel
{
    public TodoItemDetailsPageViewModel(TodoItemDetailsViewModel todoItemDetailsViewModel)
    {
        TodoItemDetailsViewModel = todoItemDetailsViewModel;
    }

    public TodoItemDetailsViewModel TodoItemDetailsViewModel { get; }
}