using expense_tracker_aspnet.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace expense_tracker_aspnet.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IConfiguration config, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(config["MongoSettings:DatabaseName"]);
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<IEnumerable<Category>> GetByUserIdAsync(string userId) =>
            await _categories.Find(c => c.UserId == userId).ToListAsync();

        public async Task<Category> GetByIdAsync(string id) =>
            await _categories.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category category) =>
            await _categories.InsertOneAsync(category);

        public async Task UpdateAsync(Category category) =>
            await _categories.ReplaceOneAsync(c => c.Id == category.Id, category);

        public async Task DeleteAsync(string id) =>
            await _categories.DeleteOneAsync(c => c.Id == id);
    }
}
