using System.Collections.Immutable;
using TodoApp.ViewModels.Navigation;
using INavigation = TodoApp.ViewModels.Navigation.INavigation;

namespace TodoApp.App;

public sealed class Navigation : INavigation
{
    public Task NavigateToAsync(Route route, IDictionary<string, object>? parameters = null, CancellationToken cancellation = default)
    {
        var appRoute = AppShell.GetRoute(route);
        var additionalParameters = parameters ?? ImmutableDictionary<string, object>.Empty;
        
        return Shell.Current.GoToAsync(appRoute, additionalParameters);
    }

    public Task GoBackAsync(CancellationToken cancellation = default)
    {
        return Shell.Current.GoToAsync("..");
    }
}