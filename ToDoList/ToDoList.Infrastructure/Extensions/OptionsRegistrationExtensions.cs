using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Infrastructure.Options;

namespace ToDoListApp.Infrastructure.Extensions;
public static class OptionsRegistrationExtensions
{
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ToDoListMongoDbOptions>(options => configuration
        .GetSection(nameof(ToDoListMongoDbOptions))
        .Bind(options));
    }
}
