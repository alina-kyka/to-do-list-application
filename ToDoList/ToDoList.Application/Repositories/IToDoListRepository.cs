using System.Linq.Expressions;
using ToDoListApp.Domain;

namespace ToDoListApp.Application.Repositories;
public interface IToDoListRepository
{
    Task CreateAsync(ToDoList entity, CancellationToken ct);
    public Task UpdateAsync(ToDoList entity, CancellationToken ct = default);
    public Task DeleteAsync(ToDoList entity, CancellationToken ct = default);
    public Task<IReadOnlyCollection<ToDoList>> SearchAsync(Expression<Func<ToDoList, bool>> predicate, int page, int pageSize, CancellationToken ct = default);
    public Task<ToDoList?> FirstOrDefaultAsync(Expression<Func<ToDoList, bool>> predicate, CancellationToken ct = default);
}