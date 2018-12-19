using Bit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.Application.Interfaces
{
   public interface IUserManager
    {
        Task<User> GetUserAsync(string userName, string password);
        Task<User> AddUserAsync(User model);
        Task<User> UpdateUserAsync(string id, User model);
    }
}
