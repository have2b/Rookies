using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IPersonService
    {
        List<Person> GetAll(string? name, GenderType? gender, string? birthPlace);
        Person? Add(PersonDTO personDTO);
        Person? Update(Guid id, PersonDTO personDTO);
        bool Delete(Guid id);
    }
}
