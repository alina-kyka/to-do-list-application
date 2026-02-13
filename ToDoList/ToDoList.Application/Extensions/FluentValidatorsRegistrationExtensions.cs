using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Models;
using ToDoListApp.Application.Validators;

namespace ToDoListApp.Application.Extensions;
public static class FluentValidatorsRegistrationExtensions
{
    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ToDoListShareModel>, ToDoListShareModelValidator>();
        services.AddScoped<IValidator<ToDoListUpdateModel>, ToDoListUpdateModelValidator>();
        services.AddScoped<IValidator<ToDoListCreateModel>, ToDoListCreateModelValidator>();
        services.AddScoped<IValidator<ToDoListSearchRequestModel>, ToDoListSearchRequestModelValidator>();
    }
}
