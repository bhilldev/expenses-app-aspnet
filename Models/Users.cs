using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpensesApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!; // MongoDB primary key

        [BsonElement("username")]
        public string Username { get; set; } = null!; // Unique username

        [BsonElement("email")]
        public string? Email { get; set; } // Optional email

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Account creation date

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Last update

        // Optional: You could store a hashed password if needed
        //[BsonElement("passwordHash")]
        //public string? PasswordHash { get; set; }
    }
}

