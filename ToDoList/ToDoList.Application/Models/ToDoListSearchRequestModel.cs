namespace ToDoListApp.Application.Models;
public record ToDoListSearchRequestModel(string UserId, int Page = 1, int PageSize = 10);
