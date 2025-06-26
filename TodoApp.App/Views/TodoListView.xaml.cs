using TodoApp.ViewModels;

namespace TodoApp.App.Views;

public partial class TodoListView : BaseContentView<TodoListViewModel>
{
    public TodoListView(TodoListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public TodoListView()
    {
        InitializeComponent();
    }
}