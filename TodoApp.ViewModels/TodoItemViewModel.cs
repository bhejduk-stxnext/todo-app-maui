using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TodoApp.ViewModels;

public partial class TodoItemViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _title = string.Empty;

    [RelayCommand]
    private async Task SaveItem(CancellationToken cancellationToken = default)
    {
        var dummy = Title == string.Empty
            ? 1
            : 0;
        
        await Task.Delay(Random.Shared.Next(100, 600), cancellationToken);
    }
}