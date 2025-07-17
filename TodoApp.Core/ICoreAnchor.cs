using System.Reflection;

namespace TodoApp.Core;

public static class CoreAssembly
{
    public static Assembly Assembly { get; } = typeof(ICoreAnchor).Assembly;
}

public interface ICoreAnchor;