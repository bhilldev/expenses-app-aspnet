namespace ExpensesApp.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        public string Password { get; set; } = null!; // Plain password for demo; hash in production
    }
}
