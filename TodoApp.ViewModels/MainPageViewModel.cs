using CommunityToolkit.Mvvm.Input;

namespace TodoApp.ViewModels;

public sealed partial class MainPageViewModel : BaseViewModel
{
    private bool _initialized;

    public MainPageViewModel(TodoListViewModel todoListViewModel)
    {
        TodoListViewModel = todoListViewModel;
    }

    public TodoListViewModel TodoListViewModel { get; }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        if (!_initialized)
            await TodoListViewModel.LoadItemsAsync();

        _initialized = true;
    }
}