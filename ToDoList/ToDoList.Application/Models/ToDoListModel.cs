namespace ToDoListApp.Application.Models;
public record ToDoListModel(string Id, string Name, string UserId, DateTime CreatedAt, HashSet<string> SharedWith);