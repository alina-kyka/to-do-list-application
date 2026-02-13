using FluentValidation;
using ToDoListApp.Application.Models;
using ToDoListApp.Application.Repositories;
using ToDoListApp.Application.Services;
using ToDoListApp.Application.Validators;
using ToDoListApp.Infrastructure.Contexts;
using ToDoListApp.Infrastructure.Options;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IToDoListService, ToDoListService>();
    }
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddSingleton<ToDoListMongoDbContext>();
    }

    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ToDoListMongoDbOptions>(options => configuration.GetSection(nameof(ToDoListMongoDbOptions)).Bind(options));
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IToDoListRepository, TodoListRepository>();
    }
    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ToDoListShareModel>, ToDoListShareModelValidator>();
        services.AddScoped<IValidator<ToDoListUpdateModel>, ToDoListUpdateModelValidator>();
        services.AddScoped<IValidator<ToDoListCreateModel>, ToDoListCreateModelValidator>();
        services.AddScoped<IValidator<ToDoListSearchRequestModel>, ToDoListSearchRequestModelValidator>();
    }
}
