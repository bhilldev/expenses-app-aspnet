using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpensesApp.Models
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("categoryId")]
        public string CategoryId { get; set; }

        [BsonElement("amountCents")]
        public int AmountCents { get; set; }

        [BsonIgnore]
        public decimal Amount
        {
            get => AmountCents / 100m;
            set => AmountCents = (int)(value * 100);
        }

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }
    }
}
