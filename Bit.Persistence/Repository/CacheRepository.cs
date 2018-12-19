using Bit.Application.Repository;
using Bit.Domain.Entities;
using Bit.Persistence.Cache;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Persistence.Repository
{
    public class CacheRepository: ICacheRepository
    {
        private readonly IDatabase _cache = null;
        public CacheRepository()
        {
            _cache = RedisConnectionContext.Connection.GetDatabase();
        }

        public async Task<Person> GetPersonFromCacheAsync(string keyName)
        {
            Person person = null;
            var personJson = await _cache.StringGetAsync(keyName);

            if (personJson.HasValue)
            {
                person = JsonConvert.DeserializeObject<Person>(personJson);
            }

            return person;
        }

        public async Task SetPersonToCacheAsync(Person person)
        {
            if (person != null)
            {
                string personJson = JsonConvert.SerializeObject(person);
                await _cache.StringSetAsync(person._id, personJson, new TimeSpan(0, 0, 30));
            }
        }

        public async Task SetPeopleToCacheAsync(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                if (person._id != null)
                {
                    await _cache.StringSetAsync(person._id, JsonConvert.SerializeObject(person), new TimeSpan(0, 0, 30));
                }
            }
        }

        public async Task RemoveKeyAsync(string keyName)
        {
            await _cache.KeyDeleteAsync(keyName);
        }
    }
}
