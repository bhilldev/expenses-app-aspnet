using System.ComponentModel.DataAnnotations;

namespace ExpensesApp.Dtos
{
    public class CreateExpenseDto
    {
        public string UserId { get; set; } = null!;   // MongoDB ObjectId as string
        public string CategoryId { get; set; } = null!;
        public int AmountCents { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
    }
}
