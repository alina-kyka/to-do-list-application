using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Repositories;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.Infrastructure.Extensions;
public static class RepositoriesRegistrationExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IToDoListRepository, TodoListRepository>();
    }
}
