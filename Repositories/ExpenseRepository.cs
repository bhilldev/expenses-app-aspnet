using ExpensesApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpensesApp.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IMongoCollection<Expense> _expenses;

        public ExpenseRepository(IMongoDatabase database)
        {
            _expenses = database.GetCollection<Expense>("Expenses");
        }

        public async Task<IEnumerable<Expense>> GetAllAsync() =>
            await _expenses.Find(_ => true).ToListAsync();

        public async Task<Expense?> GetByIdAsync(string id) =>
            await _expenses.Find(e => e.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Expense expense) =>
            await _expenses.InsertOneAsync(expense);

        public async Task UpdateAsync(string id, Expense expense) =>
            await _expenses.ReplaceOneAsync(e => e.Id == id, expense);

        public async Task DeleteAsync(string id) =>
            await _expenses.DeleteOneAsync(e => e.Id == id);

        // ===== Filtering Methods =====

        public async Task<IEnumerable<Expense>> GetLastWeekExpensesAsync()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            return await _expenses.Find(e => e.Date >= lastWeek).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetPastMonthExpensesAsync()
        {
            var pastMonth = DateTime.UtcNow.AddMonths(-1);
            return await _expenses.Find(e => e.Date >= pastMonth).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetPast3MonthsExpensesAsync()
        {
            var past3Months = DateTime.UtcNow.AddMonths(-3);
            return await _expenses.Find(e => e.Date >= past3Months).ToListAsync();
        }

        public async Task<IEnumerable<Expense>> GetExpensesByDateRangeAsync(DateTime start, DateTime end) =>
            await _expenses.Find(e => e.Date >= start && e.Date <= end).ToListAsync();
    }
}

