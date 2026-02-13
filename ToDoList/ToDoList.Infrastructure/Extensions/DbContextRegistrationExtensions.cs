using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Infrastructure.Contexts;

namespace ToDoListApp.Infrastructure.Extensions;
public static class DbContextRegistrationExtensions
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddSingleton<ToDoListMongoDbContext>();
    }
}
