using System.Net;
using ToDoListApp.Application.Exceptions;

namespace ToDoListApp.API.Extensions;

public static class ExceptionExtensions
{
    public static HttpStatusCode ParseException(this Exception exception)
    {
        return exception switch
        {
            NotFoundException _ => HttpStatusCode.NotFound,
            ForbiddenException _ => HttpStatusCode.Forbidden,
            ArgumentException _ => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
