using Bit.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Application.Interfaces
{
    public interface ICacheManager
    {
        Task SetPersonToCacheAsync(Person person);
        Task<Person> GetPersonFromCacheAsync(string keyName);
        Task SetPeopleToCacheAsync(IEnumerable<Person> people);
        Task RemoveKeyAsync(string keyName);
    }
}
