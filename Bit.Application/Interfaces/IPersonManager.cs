using Bit.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Application.Manager
{
    public interface IPersonManager
    {
        Task<Person> AddPersonAsync(Person model);
        Task<IEnumerable<Person>> GetAllPersonAsync();
        Task<Person> GetPersonAsync(string id);
        Task<Person> UpdatePersonAsync(string id, Person model);
        Task<bool> DeletePersonAsync(string id);
    }
}
