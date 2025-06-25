namespace TodoApp.Core;

public class TodoItem
{
    public string Id { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public string Details { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedDate { get; set; }
    
    public DateTimeOffset DueDate { get; set; }
    
    public bool Important { get; set; }
    
    public bool Completed { get; set; }
}