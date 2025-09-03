using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpensesApp.Models
{
    public class Category
    {
        [BsonId] // Primary key for MongoDB
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        // Optional: if categories are user-specific
        [BsonElement("userId")]
        public string? UserId { get; set; }
    }
}
