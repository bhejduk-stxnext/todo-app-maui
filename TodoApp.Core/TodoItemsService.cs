using System.Runtime.CompilerServices;

namespace TodoApp.Core;

public sealed class TodoItemsService : ITodoItemsService
{
    public async IAsyncEnumerable<TodoItem> GetAsync(
        int page = 1,
        int limit = 20,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var ids = Enumerable
            .Range(1, limit)
            .Select(x => x * page)
            .ToList();

        foreach (var id in ids)
        {
            await Task.Delay(Random.Shared.Next(100, 400), cancellationToken);

            var todoItem = new TodoItem
            {
                Id = id.ToString(),
                Title = $"Throw trash: {id}",
                DueDate = DateTimeOffset.Now.AddDays(Random.Shared.Next(20, 30)),
                CreatedDate = DateTimeOffset.Now,
                Completed = id % 3 == 0,
                Important = id % 2 == 0
            };

            yield return todoItem;
        }
    }

    public Task<string> AddAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        var id = Random.Shared.Next(1, 6000).ToString();
        return Task.FromResult(id);
    }
    

    public Task UpdateAsync(TodoItem item, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}