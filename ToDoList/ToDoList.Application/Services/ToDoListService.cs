using ToDoListApp.Application.Exceptions;
using ToDoListApp.Application.Extensions;
using ToDoListApp.Application.Models;
using ToDoListApp.Application.Repositories;
using ToDoListApp.Domain;

namespace ToDoListApp.Application.Services;
public class ToDoListService (IToDoListRepository toDoListRepository) : IToDoListService
{
    private readonly IToDoListRepository _toDoListRepository = toDoListRepository;

    public async Task CreateToDoListAsync(ToDoListCreateModel toDoList, CancellationToken ct = default)
    {
        await _toDoListRepository.CreateAsync(toDoList.ToToDoList(), ct);
    }

    public async Task DeleteToDoListAsync(string toDoListId, string userId, CancellationToken ct = default)
    {
        var toDoList = await GetToDoListIfUserHasAccess(toDoListId, userId, ct);

        if (!toDoList!.CanBeDeletedBy(userId))
            throw new ForbiddenException($"User with id {userId} cannot delete {nameof(ToDoList)} with id {toDoListId}");

        await _toDoListRepository.DeleteAsync(toDoList!, ct);
    }

    public async Task<ISet<string>> GetRelationsWithUsersAsync(string toDoListId, string userId, CancellationToken ct = default)
    {
        var toDoList = await GetToDoListIfUserHasAccess(toDoListId, userId, ct);

        return toDoList.SharedWith;
    }

    public async Task<ToDoListModel> GetToDoListByIdAsync(string id, CancellationToken ct = default)
    {
        var toDoList = await _toDoListRepository.FirstOrDefaultAsync(x => x.Id == id);

        NotFoundException.ThrowIfNull(toDoList);

        return toDoList!.ToToDoListModel();
    }

    public async Task<IReadOnlyCollection<ToDoListModel>> GetToDoListsByUserIdAsync(ToDoListSearchRequestModel model, CancellationToken ct = default)
    {
        var entities = await _toDoListRepository
            .SearchAsync(x => x.UserId == model.UserId || x.SharedWith.Contains(model.UserId), 
                model.Page, 
                model.PageSize, 
                ct);

        return entities
            .Select(x => x.ToToDoListModel())
            .ToList();
    }

    public async Task ShareWithUserAsync(ToDoListShareModel model, CancellationToken ct = default)
    {
        var toDoList = await GetToDoListIfUserHasAccess(model.ToDoListId, model.UserId, ct);

        toDoList.SharedWith.Add(model.ReceiverUserId);

        await _toDoListRepository.UpdateAsync(toDoList);
    }

    public async Task UnshareWithUserAsync(ToDoListShareModel model, CancellationToken ct = default)
    {
        var toDoList = await GetToDoListIfUserHasAccess(model.ToDoListId, model.UserId, ct);

        toDoList.SharedWith.Remove(model.ReceiverUserId);

        await _toDoListRepository.UpdateAsync(toDoList);
    }

    public async Task UpdateToDoListAsync(ToDoListUpdateModel model, CancellationToken ct = default)
    {
        var toDoList = await GetToDoListIfUserHasAccess(model.Id, model.UserId, ct);

        toDoList!.Name = model.Name;

        await _toDoListRepository.UpdateAsync(toDoList, ct);
    }

    private async Task<ToDoList> GetToDoListIfUserHasAccess(string toDoListId, string userId, CancellationToken ct = default)
    {
        var toDoList = await _toDoListRepository.FirstOrDefaultAsync(x => x.Id == toDoListId);

        NotFoundException.ThrowIfNull(toDoList);

        if (!toDoList!.CanBeAccessedBy(userId))
            throw new ForbiddenException(toDoListId, userId, nameof(ToDoList));

        return toDoList;
    }
}

public interface IToDoListService
{
    public Task<IReadOnlyCollection<ToDoListModel>> GetToDoListsByUserIdAsync(ToDoListSearchRequestModel model, CancellationToken ct);
    public Task<ToDoListModel> GetToDoListByIdAsync(string id, CancellationToken ct);
    public Task CreateToDoListAsync(ToDoListCreateModel toDoList, CancellationToken ct);
    public Task UpdateToDoListAsync(ToDoListUpdateModel toDoList, CancellationToken ct);
    public Task DeleteToDoListAsync(string toDoListId, string userId, CancellationToken ct);
    public Task ShareWithUserAsync(ToDoListShareModel model, CancellationToken ct);
    public Task UnshareWithUserAsync(ToDoListShareModel model, CancellationToken ct);
    public Task<ISet<string>> GetRelationsWithUsersAsync(string toDoListId, string userId, CancellationToken ct);
}