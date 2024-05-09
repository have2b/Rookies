using API.DTOs;
using API.Models;

namespace API.Contracts
{
    public interface IPersonService
    {
        List<Person> GetAll();
        Person? Add(PersonDTO personDTO);
        Person? Update(Guid id, PersonDTO personDTO);
        bool Delete(Guid id);
    }
}
