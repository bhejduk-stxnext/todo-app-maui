namespace TodoApp.Core;

public sealed class TodoItemsService : ITodoItemsService
{
    public async Task<IReadOnlyList<TodoItem>> GetAsync(int page = 1, int limit = 20, CancellationToken cancellationToken = default)
    {
        var ids = Enumerable
            .Range(1, limit)
            .Select(x => x * page)
            .ToList();

        var result = new List<TodoItem>();

        foreach (var id in ids)
        {
            await Task
                .Delay(Random.Shared.Next(30, 100), cancellationToken)
                .ConfigureAwait(false);

            var todoItem = new TodoItem
            {
                Id = id.ToString(),
                Title = new Bogus.DataSets.Commerce().ProductName(),
                DueDate = new Bogus.DataSets.Date().SoonOffset(),
                CreatedDate = new Bogus.DataSets.Date().RecentOffset(),
                Completed = id % 3 == 0,
                Important = id % 2 == 0
            };

            result.Add(todoItem);
        }

        return result;
    }

    public Task<string> AddAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        var id = Random
            .Shared.Next(1, 6000)
            .ToString();

        return Task.FromResult(id);
    }

    public Task UpdateAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}