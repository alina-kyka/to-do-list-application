using ToDoListApp.API.Extensions;
using ToDoListApp.API.Middleware;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

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

            builder.Services.AddFluentValidators();
            builder.Services.AddDbContext();
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddOptions(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.AddToDoListEndpoints();
            app.Run();
        }
    }
}
