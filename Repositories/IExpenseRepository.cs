using ExpensesApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpensesApp.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(string id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(string id, Expense expense);
        Task DeleteAsync(string id);

        // Filtering methods
        Task<IEnumerable<Expense>> GetLastWeekExpensesAsync();
        Task<IEnumerable<Expense>> GetPastMonthExpensesAsync();
        Task<IEnumerable<Expense>> GetPast3MonthsExpensesAsync();
        Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime start, DateTime end);
    }
}
