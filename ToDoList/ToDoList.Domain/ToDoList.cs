namespace ToDoListApp.Domain;
public class ToDoList
{
    public ToDoList(string id, string name, string userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
    }

    public ToDoList(string name, string userId)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        UserId = userId;
    }

    public string Id { get; private set; }
    public string Name { get; set; }
    public string UserId { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public HashSet<string> SharedWith { get; private set; } = [];
    public bool CanBeAccessedBy(string userId) => UserId.Equals(userId) || SharedWith.Contains(userId);
    public bool CanBeDeletedBy(string userId) => UserId.Equals(userId);
}