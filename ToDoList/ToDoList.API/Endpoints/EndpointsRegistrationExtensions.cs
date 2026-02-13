using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using ToDoListApp.Application.Models;
using ToDoListApp.Application.Services;

namespace ToDoListApp.API.Endpoints;

public static class EndpointsRegistrationExtensions
{
    public static void AddToDoListEndpoints(this WebApplication app)
    {
        app.MapPost($"{Routes.Root}/{Routes.ToDoList.Base}", async (CancellationToken ct,
            [FromServices] IValidator<ToDoListCreateModel> validator,
            [FromServices] IToDoListService toDoListService,
            [FromBody] ToDoListCreateModel toDoList) =>
        {
            await toDoListService.CreateToDoListAsync(toDoList,ct);
            return Results.Created();
        })
            .AddFluentValidationAutoValidation()
            .Produces(StatusCodes.Status201Created);

        app.MapGet($"{Routes.Root}/{Routes.ToDoList.Base}/{{id}}", async (CancellationToken ct,
            [FromServices] IToDoListService toDoListService,
            [FromRoute] string id) =>
        {
            return Results.Ok(await toDoListService.GetToDoListByIdAsync(id, ct));
        })
            .Produces<ToDoListModel>(StatusCodes.Status200OK);

        app.MapGet($"{Routes.Root}/{Routes.ToDoList.Base}", async (CancellationToken ct,
            [FromServices] IValidator<ToDoListCreateModel> validator,
            [FromServices] IToDoListService toDoListService,
            [AsParameters] ToDoListSearchRequestModel model) =>
        {
            return Results.Ok(await toDoListService.GetToDoListsByUserIdAsync(model, ct));
        })
            .AddFluentValidationAutoValidation()
            .Produces<IReadOnlyCollection<ToDoListModel>>(StatusCodes.Status200OK);

        app.MapGet($"{Routes.Root}/{Routes.ToDoList.Base}/{{id}}/{Routes.ToDoList.Relations}", async (CancellationToken ct,
            [FromServices] IToDoListService toDoListService,
            [FromRoute] string id,
            [FromQuery] string userId) =>
        {
            return Results.Ok(await toDoListService.GetRelationsWithUsersAsync(id, userId, ct));
        })
            .Produces<ISet<string>>(StatusCodes.Status200OK);

        app.MapPut($"{Routes.Root}/{Routes.ToDoList.Base}", async (CancellationToken ct,
            [FromServices] IValidator<ToDoListUpdateModel> validator,
            [FromServices] IToDoListService toDoListService,
            [FromBody] ToDoListUpdateModel toDoList) =>
        {
            await toDoListService.UpdateToDoListAsync(toDoList, ct);
            return Results.Ok();
        })
            .AddFluentValidationAutoValidation()
            .Produces(StatusCodes.Status200OK);

        app.MapPatch($"{Routes.Root}/{Routes.ToDoList.Base}/{Routes.ToDoList.Share}", async (CancellationToken ct,
            [FromServices] IValidator<ToDoListShareModel> validator,
            [FromServices] IToDoListService toDoListService,
            [FromBody] ToDoListShareModel model) =>
        {
            await toDoListService.ShareWithUserAsync(model, ct);
            return Results.Ok();
        })
            .AddFluentValidationAutoValidation()
            .Produces(StatusCodes.Status200OK);

        app.MapPatch($"{Routes.Root}/{Routes.ToDoList.Base}/{Routes.ToDoList.Unshare}", async (CancellationToken ct,
            [FromServices] IValidator<ToDoListShareModel> validator,
            [FromServices] IToDoListService toDoListService,
            [FromBody] ToDoListShareModel model) =>
        {
            await toDoListService.UnshareWithUserAsync(model, ct);
            return Results.Ok();
        })
            .AddFluentValidationAutoValidation()
            .Produces(StatusCodes.Status200OK);

        app.MapDelete($"{Routes.Root}/{Routes.ToDoList.Base}/{{id}}", async (CancellationToken ct,
            [FromServices] IToDoListService toDoListService,
            [FromRoute] string id,
            [FromQuery] string userId) =>
        {
            await toDoListService.DeleteToDoListAsync(id, userId, ct);
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent);
    }
}
