using ExpensesApp.Models;
using MongoDB.Driver;

namespace ExpensesApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<List<Category>> GetAllAsync() =>
            await _categories.Find(_ => true).ToListAsync();

        public async Task<Category?> GetByIdAsync(string id) =>
            await _categories.Find(c => c.CategoryId == id).FirstOrDefaultAsync();

        public async Task AddAsync(Category category) =>
            await _categories.InsertOneAsync(category);

        public async Task DeleteAsync(string id) =>
            await _categories.DeleteOneAsync(c => c.CategoryId == id);
    }
}
