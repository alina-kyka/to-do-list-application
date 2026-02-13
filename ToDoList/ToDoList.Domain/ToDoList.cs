namespace ToDoListApp.Domain;
public class ToDoList
{
    private const int MIN_NAME_LENGTH = 1;
    private const int MAX_NAME_LENGTH = 255;
    private string name;

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
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameof(Name)} cannot be empty.");

            if (value.Length < MIN_NAME_LENGTH || value.Length > MAX_NAME_LENGTH)
                throw new ArgumentException($"Length of {nameof(Name)} must be greater than {MIN_NAME_LENGTH} and less than {MAX_NAME_LENGTH}.");

            name = value;
        }
    }

    public string UserId { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public HashSet<string> SharedWith { get; private set; } = [];

    public bool CanBeAccessedBy(string userId) => UserId.Equals(userId) || SharedWith.Contains(userId);
    public bool CanBeDeletedBy(string userId) => UserId.Equals(userId);
}