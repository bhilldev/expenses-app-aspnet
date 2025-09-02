using ExpensesApp.Models;

namespace ExpensesApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);
        Task AddAsync(Category category);
        Task DeleteAsync(string id);
    }
}
