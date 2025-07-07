using ISnackbar = TodoApp.ViewModels.ISnackbar;

namespace TodoApp.App;

public class Snackbar : ISnackbar
{
    public Task ShowAsync(string message)
    {
        return CommunityToolkit
            .Maui.Alerts.Snackbar.Make(message)
            .Show();
    }
}