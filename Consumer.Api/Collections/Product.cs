using MongoDB.Bson.Serialization.Attributes;

namespace Consumer.Api.Collections;

public class Product
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
}