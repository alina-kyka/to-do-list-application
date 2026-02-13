namespace ToDoListApp.Application.Models;
public record ToDoListSearchResponseModel(IReadOnlyCollection<ToDoListModel> ToDoLists, int Page);
