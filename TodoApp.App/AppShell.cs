using TodoApp.App.Pages;

namespace TodoApp.App;

public sealed class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        Items.Add(mainPage);

        RegisterRoutes();
    }

    public static string GetRoute<T>()
        where T : Page
    {
        var pageType = typeof(T);

        return pageType switch
        {
            _ when pageType == typeof(MainPage) => $"//{nameof(MainPage)}",
            _ => throw new NotSupportedException($"Page {pageType} Not Found in Routing Table")
        };
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(GetRoute<MainPage>(), typeof(MainPage));
    }
}