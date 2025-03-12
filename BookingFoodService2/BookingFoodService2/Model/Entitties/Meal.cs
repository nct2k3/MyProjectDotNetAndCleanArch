using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookingFoodServie2.Model.Entitties;

public class Meal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    
    public string Name { get; set; } = null!;
    
    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("category")]
    public string Category { get; set; }

    [BsonElement("isAvailable")]
    public bool IsAvailable { get; set; } = true;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}