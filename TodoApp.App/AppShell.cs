using TodoApp.App.Pages;
using TodoApp.ViewModels.Navigation;

namespace TodoApp.App;

// ReSharper disable PartialTypeWithSinglePart
public sealed partial class AppShell : Shell
{
    public AppShell(MainPage mainPage)
    {
        SetNavBarIsVisible(this, false);

        Items.Add(mainPage);

        RegisterRoutes();
    }

    public static string GetRoute(Route route)
    {
        return route switch
        {
            MainPageRoute => GetRoute<MainPage>(),
            NewTodoItemRoute => GetRoute<NewTodoItemPage>(),
            EditTodoItemRoute => GetRoute<EditTodoItemPage>(),
            _ => throw new NotSupportedException($"Route {route.GetType().Name} not found in Routing Table")
        };
    }

    public static string GetRoute<TPage>()
        where TPage : Page
    {
        var pageType = typeof(TPage);

        return pageType switch
        {
            _ when pageType == typeof(MainPage) => $"//{nameof(MainPage)}",
            _ when pageType == typeof(NewTodoItemPage) => $"//{nameof(MainPage)}/{nameof(NewTodoItemPage)}",
            _ when pageType == typeof(EditTodoItemPage) => $"//{nameof(MainPage)}/{nameof(EditTodoItemPage)}",
            _ => throw new NotSupportedException($"Page {pageType} not found in Routing Table")
        };
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute(GetRoute<MainPage>(), typeof(MainPage));
        Routing.RegisterRoute(GetRoute<NewTodoItemPage>(), typeof(NewTodoItemPage));
        Routing.RegisterRoute(GetRoute<EditTodoItemPage>(), typeof(EditTodoItemPage));
    }
}