using System.Runtime.CompilerServices;

namespace ToDoListApp.Application.Exceptions;
public class NotFoundException: Exception
{
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string id, string entityType) : base($"{entityType} with id {id} not found.") { }

    public static void ThrowIfNull(object? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument == null)
        {
            throw new NotFoundException($"Entity corresponding to '{paramName}' was not found.");
        }
    }
}
