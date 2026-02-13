using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using ToDoListApp.API.Common;
using ToDoListApp.API.Endpoints;
using ToDoListApp.Application.Extensions;
using ToDoListApp.Infrastructure.Extensions;

namespace ToDoListApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddFluentValidators();
            builder.Services.AddDbContext();
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddOptions(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandler();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.AddToDoListEndpoints();
            app.Run();
        }
    }
}
