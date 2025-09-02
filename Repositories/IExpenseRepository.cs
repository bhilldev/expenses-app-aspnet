// Models/IExpenseRepository.cs
using expense_tracker_aspnet.Models;

public interface IExpenseRepository
{
    Task<List<Expense>> GetExpensesAsync(string userId);
    Task<Expense?> GetExpenseAsync(string id);
    Task AddExpenseAsync(Expense expense);
    Task DeleteExpenseAsync(string id);
}
