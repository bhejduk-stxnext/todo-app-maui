using CommunityToolkit.Mvvm.Input;
using TodoApp.ViewModels.Navigation;
using TodoApp.ViewModels.Views;

namespace TodoApp.ViewModels.Pages;

public sealed partial class MainPageViewModel : BaseViewModel
{
    private readonly INavigation _navigation;

    private bool _initialized;

    public MainPageViewModel(TodoListViewModel todoListViewModel, INavigation navigation)
    {
        TodoListViewModel = todoListViewModel;
        _navigation = navigation;
    }

    public TodoListViewModel TodoListViewModel { get; }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        if (!_initialized)
            await TodoListViewModel.LoadItemsAsync().ConfigureAwait(false);

        _initialized = true;
    }

    [RelayCommand]
    private Task CreateNewItemAsync(CancellationToken cancellationToken)
    {
        return _navigation.NavigateToAsync(Routes.NewTodoItem, cancellation: cancellationToken);
    }
}