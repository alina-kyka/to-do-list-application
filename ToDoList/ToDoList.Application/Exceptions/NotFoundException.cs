namespace ToDoListApp.Application.Exceptions;
public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string id, string entityType) : base($"{entityType} with id {id} not found.") { }
}
