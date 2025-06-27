using TodoApp.Core;
using TodoApp.ViewModels.Pages;

namespace TodoApp.App.Pages;

public partial class TodoItemDetailsPage : BaseContentPage<TodoItemDetailsPageViewModel>, IQueryAttributable
{
    public TodoItemDetailsPage(TodoItemDetailsPageViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var todoItem = (TodoItem)query["TodoItem"];
        ViewModel.TodoItemDetailsViewModel.TodoItem = todoItem;
    }
}