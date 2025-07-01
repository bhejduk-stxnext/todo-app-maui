namespace TodoApp.Core;

public sealed class Category
{
    public string Id { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
}

public static class Categories
{
    public static readonly Category Work = new Category
    {
        Id = "1",
        Name = "Work"
    };
    
    public static readonly Category Home = new Category
    {
        Id = "2",
        Name = "Home"
    };
    
    public static readonly Category Fun = new Category
    {
        Id = "3",
        Name = "Fun"
    };

    public static IEnumerable<Category> GetAll()
    {
        yield return Work;
        yield return Home;
        yield return Fun;
    }
}