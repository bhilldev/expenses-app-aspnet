using ExpensesApp.Models;

namespace ExpensesApp.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(string id);
        Task AddAsync(Expense expense);
        Task DeleteAsync(string id);
    }
}
