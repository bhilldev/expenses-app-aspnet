public class CreateExpenseDto
{
    public string UserId { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public int AmountCents { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
}
