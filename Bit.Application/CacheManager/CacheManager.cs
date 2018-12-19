using Bit.Application.Interfaces;
using Bit.Application.Repository;
using Bit.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Application.CacheManager
{
    public class CacheManager : ICacheManager
    {
        private readonly ICacheRepository _repo = null;
        public CacheManager(ICacheRepository repo)
        {
            _repo = repo;
        }

        public async Task<Person> GetPersonFromCacheAsync(string keyName)
        {
            var personJson = await _repo.GetPersonFromCacheAsync(keyName);
            return personJson;
        }

        public async Task SetPersonToCacheAsync(Person person)
        {
            await _repo.SetPersonToCacheAsync(person);
        }

        public async Task SetPeopleToCacheAsync(IEnumerable<Person> people)
        {
            await _repo.SetPeopleToCacheAsync(people);
        }

        public async Task RemoveKeyAsync(string keyName)
        {
            await _repo.RemoveKeyAsync(keyName);
        }
    }
}
