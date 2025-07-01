namespace TodoApp.Core;

public interface ITodoItemsService
{
    public Task<IReadOnlyList<TodoItem>> GetAsync(int page = 1, int limit = 20, CancellationToken cancellationToken = default);

    public Task<string> AddAsync(TodoItem item, CancellationToken cancellationToken = default);

    public Task UpdateAsync(TodoItem item, CancellationToken cancellationToken = default);

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    
    public Task<IReadOnlyList<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
}