using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Services;

namespace ToDoListApp.Application.Extensions;
public static class ServicesRegistrationExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoListService, ToDoListService>();
    }
}
