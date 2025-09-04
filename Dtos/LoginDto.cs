namespace ExpensesApp.Dtos;
public class LoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!; // In production, store and check hashed passwords
}
