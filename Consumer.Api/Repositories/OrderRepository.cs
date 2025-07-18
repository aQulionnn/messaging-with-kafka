using Consumer.Api.Collections;
using Consumer.Api.Data;
using Consumer.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Consumer.Api.Repositories;

public class OrderRepository(MongoDbContext context) 
    : IOrderRepository
{
    private readonly IMongoCollection<Order> _collection = context.GetCollection<Order>();
    
    public async Task<Order> InsertAsync(Order order)
    {
        await _collection.InsertOneAsync(order);
        return order;
    }

    public async Task<Order?> GetByIdAsync(string id)
    {
        var orderId = new ObjectId(id);
        var order = await _collection.Find(Builders<Order>.Filter.Eq("_id", orderId)).FirstOrDefaultAsync();
        return order;
    }
}

public interface IOrderRepository
{
    Task<Order> InsertAsync(Order order);
    Task<Order?> GetByIdAsync(string id);
}