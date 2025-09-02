using System.Collections.Generic;
using System.Threading.Tasks;
using expense_tracker_aspnet.Models;

namespace expense_tracker_aspnet.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetByUserIdAsync(string userId);
        Task<Category> GetByIdAsync(string id);
        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(string id);
    }
}
