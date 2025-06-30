using TodoApp.ViewModels.Pages;

namespace TodoApp.App.Pages;

public partial class NewTodoItemPage : BaseContentPage<NewTodoViewModel>
{
    public NewTodoItemPage(NewTodoViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}