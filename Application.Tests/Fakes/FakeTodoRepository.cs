using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Tests.Fakes;

public class FakeTodoRepository : IGenericRepository<TodoItem>
{
    private readonly List<TodoItem> _items = new();
    private int _nextId = 1;

    public Task<TodoItem?> GetByIdAsync(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(item);
    }

    public Task<IReadOnlyList<TodoItem>> ListAsync()
    {
        return Task.FromResult((IReadOnlyList<TodoItem>)_items.ToList());
    }

    public Task<IReadOnlyList<TodoItem>> ListAsync(Expression<Func<TodoItem, bool>> predicate)
    {
        var compiled = predicate.Compile();
        var result = _items.Where(compiled).ToList();
        return Task.FromResult((IReadOnlyList<TodoItem>)result);
    }

    public Task<TodoItem> AddAsync(TodoItem entity)
    {
        entity.Id = _nextId++;
        _items.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(TodoItem entity)
    {
        var existing = _items.FirstOrDefault(x => x.Id == entity.Id);
        if (existing is not null)
        {
            var index = _items.IndexOf(existing);
            _items[index] = entity;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(TodoItem entity)
    {
        _items.RemoveAll(x => x.Id == entity.Id);
        return Task.CompletedTask;
    }
}
