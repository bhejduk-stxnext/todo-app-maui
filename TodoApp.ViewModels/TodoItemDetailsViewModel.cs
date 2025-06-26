using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TodoApp.Core;

namespace TodoApp.ViewModels;

public partial class TodoItemDetailsViewModel : BaseViewModel
{
    private readonly ITodoItemsService _todoItemsService;

    [ObservableProperty]
    private string _title = string.Empty;

    public TodoItemDetailsViewModel(ITodoItemsService todoItemsService)
    {
        _todoItemsService = todoItemsService;
    }

    [RelayCommand]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        var dummy = Title == string.Empty
            ? 1
            : 0;

        await Task.Delay(Random.Shared.Next(100, 600), cancellationToken);
    }
}