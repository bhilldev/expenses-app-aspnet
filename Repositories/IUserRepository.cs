using ExpensesApp.Models;

namespace ExpensesApp.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task CreateAsync(User user);
        Task UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
    }
}
