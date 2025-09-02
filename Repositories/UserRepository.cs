using ExpensesApp.Models;
using MongoDB.Driver;

namespace ExpensesApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dbName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.UserId == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task UpdateAsync(string id, User user)
        {
            await _users.ReplaceOneAsync(u => u.UserId == id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _users.DeleteOneAsync(u => u.UserId == id);
        }
    }
}
