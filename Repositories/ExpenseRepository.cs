// Repositories/ExpenseRepository.cs
using MongoDB.Driver;
using expense_tracker_aspnet.Models;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IMongoCollection<Expense> _expenses;

    public ExpenseRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("ExpenseTrackerDb");
        _expenses = database.GetCollection<Expense>("Expenses");
    }

    public async Task<List<Expense>> GetExpensesAsync(string userId)
    {
        return await _expenses.Find(e => e.UserId == userId).ToListAsync();
    }

    public async Task<Expense?> GetExpenseAsync(string id)
    {
        return await _expenses.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        await _expenses.InsertOneAsync(expense);
    }

    public async Task DeleteExpenseAsync(string id)
    {
        await _expenses.DeleteOneAsync(e => e.Id == id);
    }
}
