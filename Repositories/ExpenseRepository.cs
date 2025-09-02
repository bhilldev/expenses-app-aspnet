using ExpensesApp.Models;
using MongoDB.Driver;

namespace ExpensesApp.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IMongoCollection<Expense> _expenses;

        public ExpenseRepository(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _expenses = database.GetCollection<Expense>("Expenses");
        }

        public async Task<List<Expense>> GetAllAsync() =>
            await _expenses.Find(_ => true).ToListAsync();

        public async Task<Expense?> GetByIdAsync(string id) =>
            await _expenses.Find(e => e.ExpenseId == id).FirstOrDefaultAsync();

        public async Task AddAsync(Expense expense) =>
            await _expenses.InsertOneAsync(expense);

        public async Task DeleteAsync(string id) =>
            await _expenses.DeleteOneAsync(e => e.ExpenseId == id);
    }
}
