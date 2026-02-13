using System.Linq.Expressions;

namespace ToDoListApp.Application.Repositories;
public interface IRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken ct);
    public Task UpdateAsync(T entity, CancellationToken ct = default);
    public Task DeleteAsync(T entity, CancellationToken ct = default);
    public Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate, int page, int amount, CancellationToken ct = default);
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
}