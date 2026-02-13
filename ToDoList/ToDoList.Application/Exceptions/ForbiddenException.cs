namespace ToDoListApp.Application.Exceptions;
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
    public ForbiddenException(string resourceId, string userId, string entityType) : 
        base($"Access to {entityType} with id {resourceId} denied for user with id {userId}") { }
}
