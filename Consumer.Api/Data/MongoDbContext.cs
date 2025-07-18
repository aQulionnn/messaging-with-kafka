using Consumer.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Consumer.Api.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<DatabaseSettings> settings)
    {
        var client  = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.Name);
    }
    
    public IMongoCollection<T> GetCollection<T>(string? name = null) =>
        _database.GetCollection<T>(name ?? typeof(T).Name);
}