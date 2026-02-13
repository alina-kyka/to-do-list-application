using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using ToDoListApp.Application.Repositories;
using ToDoListApp.Domain;
using ToDoListApp.Infrastructure.Contexts;

namespace ToDoListApp.Infrastructure.Repositories;
public class TodoListRepository (ToDoListMongoDbContext context) : IRepository<ToDoList>
{
    private readonly ToDoListMongoDbContext _context = context;
    private IMongoCollection<ToDoList> _collection => _context.ToDoLists;

    public async Task CreateAsync(ToDoList entity, CancellationToken ct = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: ct);
    }

    public async Task DeleteAsync(ToDoList entity, CancellationToken ct = default)
    {
        await _collection.DeleteOneAsync(x => x.Id == entity.Id, cancellationToken: ct);
    }

    public async Task<ToDoList?> FirstOrDefaultAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken ct = default)
    {
        return await _collection.AsQueryable().FirstOrDefaultAsync(predicate, ct);
    }

    public async Task<IEnumerable<ToDoList>> SearchAsync(Expression<Func<ToDoList, bool>> predicate, int page, int amount, CancellationToken ct = default)
    {
        return await _collection.AsQueryable()
            .Where(predicate)
            .OrderByDescending(x => x.CreatedAt)
            .Skip(page * amount)
            .Take(amount)
            .ToListAsync();
    }

    public async Task UpdateAsync(ToDoList entity, CancellationToken ct = default)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: ct);
    }
}