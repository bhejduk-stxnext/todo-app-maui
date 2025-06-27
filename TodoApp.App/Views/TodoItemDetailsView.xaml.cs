using TodoApp.ViewModels;

namespace TodoApp.App.Views;

public partial class TodoItemDetailsView : BaseContentView<TodoItemDetailsViewModel>
{
    public TodoItemDetailsView(TodoItemDetailsViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public TodoItemDetailsView()
    {
        InitializeComponent();
    }
}