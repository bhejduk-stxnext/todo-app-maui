namespace TodoApp.ViewModels.Navigation;

/// <summary>
///     Supports navigation to different pages.
/// </summary>
public interface INavigation
{
    /// <summary>
    ///     Navigates to given route.
    /// </summary>
    public Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null, CancellationToken cancellation = default);

    /// <summary>
    ///     Goes back to previous page.
    /// </summary>
    public Task GoBackAsync(CancellationToken cancellation = default);
}