using ToDoListApp.Application.Exceptions;

namespace ToDoListApp.Application.Extensions;
public static class EntityExtensions
{
    public static T EnsureFound<T>(this T? entity, string id, string entityName) where T : class
    {
        if (entity == null) throw new NotFoundException(id, entityName);

        return entity;
    }
}
