using Consumer.Api.Collections;
using Consumer.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Consumer.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _collection;

    public ProductRepository(IOptions<DatabaseSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.Name);
        
        _collection = database.GetCollection<Product>(nameof(Product));
    }
    
    public async Task<Product> InsertAsync(Product collection)
    {
        await _collection.InsertOneAsync(collection);
        return collection;
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var productId = new ObjectId(id);
        var product = await _collection.Find(Builders<Product>.Filter.Eq("_id", productId)).FirstOrDefaultAsync();
        return product;
    }
}

public interface IProductRepository
{
    Task<Product> InsertAsync(Product product);
    Task<Product?> GetByIdAsync(string id);
}