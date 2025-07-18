using MongoDB.Bson.Serialization.Attributes;

namespace Consumer.Api.Collections;

public class Order
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public required string Id { get; set; }
    public required string CustomerName { get; set; }
    public double Sum { get; set; }
}