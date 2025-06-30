using TodoApp.ViewModels.Views;

namespace TodoApp.App.Views;

public partial class TodoItemFormView : BaseContentView<TodoItemFormViewModel>
{
    public TodoItemFormView(TodoItemFormViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public TodoItemFormView()
    {
        InitializeComponent();
    }
}