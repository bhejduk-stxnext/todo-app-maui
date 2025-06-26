using INavigation = TodoApp.ViewModels.Navigation.INavigation;

namespace TodoApp.App;

public sealed class Navigation : INavigation
{
    private readonly AppShell _appShell;

    public Navigation(AppShell appShell)
    {
        _appShell = appShell;
    }

    public Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null, CancellationToken cancellation = default)
    {
        return _appShell.GoToAsync(route, parameters);
    }

    public Task GoBackAsync(CancellationToken cancellation = default)
    {
        return _appShell.GoToAsync("..");
    }
}