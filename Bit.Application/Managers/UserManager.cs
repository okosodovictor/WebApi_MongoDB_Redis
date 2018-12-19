using Bit.Application.Interfaces;
using Bit.Application.Repository;
using Bit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bit.Application.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repo;
        private readonly IEncryption _enc;
        public UserManager(IUserRepository repo, IEncryption enc)
        {
            _repo = repo;
            _enc = enc;
        }

        public async Task<User> AddUserAsync(User model)
        {
            var password = await _enc.Encrypt(model.Password);
            var newUser = new User
            {
                UserName = model.UserName,
                Password = password
            };

            var user = await _repo.CreateUserAsync(newUser);

            return user;
        }

        public async Task<User> GetUserAsync(string userName, string password)
        {
            var encPassword = await _enc.Encrypt(password);
            var user = await _repo.GetUserAsync(userName, encPassword);

            return user;
        }

        public async Task<User> UpdateUserAsync(string id, User model)
        {
            User user = await _repo.UpdateUserAsync(id, model);

            return user;
        }
    }
}
