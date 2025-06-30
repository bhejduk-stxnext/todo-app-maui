namespace TodoApp.ViewModels;

public interface ISnackbar
{
    public Task ShowAsync(string message);
}