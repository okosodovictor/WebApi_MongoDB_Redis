using System.Threading.Tasks;
using Bit.Application.Repository;
using Bit.Domain.Entities;
using MongoDB.Driver;
using System.Linq;

namespace Bit.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetUserAsync(string userName, string password)
        {
            var user = await GetMongoConext.UserCollection
                                           .Find(x => x.UserName == userName && x.Password == password)
                                           .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> CreateUserAsync(User model)
        {
            await GetMongoConext.UserCollection.InsertOneAsync(model);
            return model;
        }

        public async Task<User> UpdateUserAsync(string Id, User model)
        {
            var filter = Builders<User>.Filter.Eq("_id", Id);

            var user = await GetMongoConext.UserCollection.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                await GetMongoConext.UserCollection.ReplaceOneAsync(u => u._id == model._id, model, new UpdateOptions { IsUpsert = true });
            }

            return model;
        }

        public async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("_id", id);

            var user = await GetMongoConext.UserCollection.Find(filter).FirstOrDefaultAsync();

            return user;
        }

        private MongodbContext GetMongoConext => new MongodbContext();
    }
}
