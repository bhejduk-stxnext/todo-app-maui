namespace TodoApp.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    public MainPageViewModel(TodoListViewModel todoListViewModel)
    {
        TodoListViewModel = todoListViewModel;
    }

    public TodoListViewModel TodoListViewModel { get; }
}