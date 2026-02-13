using ToDoListApp.Application.Models;
using ToDoListApp.Domain;

namespace ToDoListApp.Application.Mapping;
public static class ToDoListMappingExtensions
{
    public static ToDoList ToToDoList(this ToDoListCreateModel model)
    {
        return new ToDoList (model.Name, model.UserId);
    }

    public static ToDoListModel ToToDoListModel(this ToDoList entity)
    {
        return new ToDoListModel(entity.Id, entity.Name, entity.UserId, entity.CreatedAt, entity.SharedWith);
    }
}
