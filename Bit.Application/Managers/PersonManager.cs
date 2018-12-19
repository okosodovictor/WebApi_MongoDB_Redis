using Bit.Application.Manager;
using Bit.Application.Repository;
using Bit.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bit.Application.PersonManager
{
    public class PersonManager : IPersonManager
    {
        private readonly IPersonRepository _repo;
        public PersonManager(IPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task<Person> AddPersonAsync(Person model)
        {
            await _repo.AddPersonAsync(model);
            return model;
        }

        public async Task<bool> DeletePersonAsync(string id)
        {    
            var result = await _repo.DeletePersonAsync(id);
            return result;
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            var result = await _repo.GetAllPersonAsync();
            return result;
        }

        public async Task<Person> GetPersonAsync(string id)
        {
            var person = await _repo.GetPersonAsyn(id);
            return person;
        }

        public async Task<Person> UpdatePersonAsync(string id, Person model)
        {
            var person = await _repo.UpdatePersonAsync(id, model);
            return person;
        }
    }
}
