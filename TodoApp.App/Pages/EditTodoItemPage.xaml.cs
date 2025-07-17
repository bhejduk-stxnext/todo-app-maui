using TodoApp.Core;
using TodoApp.ViewModels.Pages;

namespace TodoApp.App.Pages;

public sealed partial class EditTodoItemPage : BaseContentPage<EditTodoItemViewModel>, IQueryAttributable
{
    public EditTodoItemPage(EditTodoItemViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var todoItem = (TodoItem)query["TodoItem"];
        ViewModel.TodoItem = todoItem;
    }
}