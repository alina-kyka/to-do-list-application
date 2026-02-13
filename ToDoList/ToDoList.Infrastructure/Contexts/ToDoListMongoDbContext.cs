using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoListApp.Domain;
using ToDoListApp.Infrastructure.Options;

namespace ToDoListApp.Infrastructure.Contexts;
public class ToDoListMongoDbContext
{
    private IMongoDatabase _mongoDatabase;

    public ToDoListMongoDbContext(IOptions<ToDoListMongoDbOptions> options)
    {
        _mongoDatabase = new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.Name);
    }

    public IMongoCollection<ToDoList> ToDoLists => _mongoDatabase.GetCollection<ToDoList>(nameof(ToDoLists));
}
