using TodoApp.Core;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Factories;

public interface ITodoListItemSummaryViewModelFactory
{
    public TodoListItemSummaryViewModel Create(TodoItem todoItem);
}