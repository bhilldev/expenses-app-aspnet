using ExpensesApp.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpensesApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<User?> GetByIdAsync(string id) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(User user) =>
            await _users.InsertOneAsync(user);

        public async Task UpdateAsync(string id, User user) =>
            await _users.ReplaceOneAsync(u => u.Id == id, user);

        public async Task DeleteAsync(string id) =>
            await _users.DeleteOneAsync(u => u.Id == id);
        public async Task<User?> GetByUsernameAsync(string username) =>
        await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
    }
}
