namespace TodoApp.ViewModels.Navigation;

public abstract record Route;

public record MainPageRoute : Route
{
    private MainPageRoute() { }

    public static MainPageRoute Instance { get; } = new();
}

public record TodoItemDetailsRoute : Route
{
    private TodoItemDetailsRoute() { }

    public static TodoItemDetailsRoute Instance { get; } = new();
}

public static class Routes
{
    public static Route MainPage => MainPageRoute.Instance;

    public static Route TodoItemDetails => TodoItemDetailsRoute.Instance;
}