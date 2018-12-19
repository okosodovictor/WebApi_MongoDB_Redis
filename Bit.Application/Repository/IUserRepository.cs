using Bit.Domain.Entities;
using System.Threading.Tasks;

namespace Bit.Application.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username, string password);
        Task<User> CreateUserAsync(User model);
        Task<User> UpdateUserAsync(string id, User model);
        Task<User> GetUserAsync(string id);
    }
}
