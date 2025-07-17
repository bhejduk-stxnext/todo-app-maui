namespace TodoApp.Core;

public sealed class TodoItem
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public Category? Category { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset? Deadline { get; set; }

    public bool Important { get; set; }

    public bool Completed { get; set; }
}