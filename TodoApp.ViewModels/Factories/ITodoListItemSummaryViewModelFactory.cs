using TodoApp.Core;

namespace TodoApp.ViewModels.Factories;

public interface ITodoListItemSummaryViewModelFactory
{
    public TodoListItemSummaryViewModel Create(TodoItem todoItem);
}