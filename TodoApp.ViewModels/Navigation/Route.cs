namespace TodoApp.ViewModels.Navigation;

public abstract record Route;

public record MainPageRoute : Route
{
    private MainPageRoute() { }

    public static MainPageRoute Instance { get; } = new();
}

public record NewTodoItemRoute : Route
{
    private NewTodoItemRoute() { }

    public static NewTodoItemRoute Instance { get; } = new();
}

public record EditTodoItemRoute : Route
{
    private EditTodoItemRoute() { }

    public static EditTodoItemRoute Instance { get; } = new();
}

public static class Routes
{
    public static Route MainPage => MainPageRoute.Instance;

    public static Route NewTodoItem => NewTodoItemRoute.Instance;

    public static Route EditTodoItem => EditTodoItemRoute.Instance;
}