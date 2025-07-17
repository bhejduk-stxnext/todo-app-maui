namespace TodoApp.Core;

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