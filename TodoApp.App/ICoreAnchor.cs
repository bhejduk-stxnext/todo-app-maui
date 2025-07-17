using System.Reflection;

namespace TodoApp.App;

public static class AppAssembly
{
    public static Assembly Assembly { get; } = typeof(IAppAnchor).Assembly;
}

public interface IAppAnchor;