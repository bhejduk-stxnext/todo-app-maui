using TodoApp.Core;

namespace TodoApp.ViewModels.Models;

public sealed class NoneCategory
{
    public static Category Instance { get; } = new Category();
}