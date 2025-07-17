using System.Reflection;

namespace TodoApp.ViewModels;

public static class ViewModelsAssembly
{
    public static Assembly Assembly { get; } = typeof(IViewModelsAnchor).Assembly;
}

public interface IViewModelsAnchor;