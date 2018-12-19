using Bit.Application.Repository;
using Bit.Domain.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bit.Persistence.Repository
{
    public class PersonRepository: IPersonRepository
    {
        public async Task<Person> AddPersonAsync(Person model)
        {
            await GetMongoConext.personCollection.InsertOneAsync(model);
            return model;
        }

        public async Task<bool> DeletePersonAsync(string id)
        {
            var filter = Builders<Person>.Filter.Eq("_id", id);
            var result = await GetMongoConext.personCollection.DeleteOneAsync(filter);
            if (result.IsAcknowledged && result.DeletedCount == 1) return true;
            return false;
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            var result = await GetMongoConext.personCollection.AsQueryable().ToListAsync();
            return result;
        }

        public async Task<Person> GetPersonAsyn(string id)
        {
            var filter = Builders<Person>.Filter.Eq("_id", id);
            var person = await GetMongoConext.personCollection.Find(filter).FirstOrDefaultAsync();

            return person;
        }

        public async Task<Person> UpdatePersonAsync(string id, Person model)
        {
            var filter = Builders<Person>.Filter.Eq("_id", id);
            var person = await GetMongoConext.personCollection.Find(filter).FirstOrDefaultAsync();
            if (person != null)
            {
                await GetMongoConext.personCollection.ReplaceOneAsync(item => item._id == id, model, new UpdateOptions { IsUpsert = true });
            }

            return model;
        }

        private MongodbContext GetMongoConext => new MongodbContext();
    }
}
