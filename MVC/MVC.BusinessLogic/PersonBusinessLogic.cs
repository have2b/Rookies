using MVC.Models.Models;
using MVC.Models.Repositories;

namespace MVC.BusinessLogic
{
    public class PersonBusinessLogic : IPersonBusinessLogic
    {
        private readonly IPersonRepository _personRepository;

        public PersonBusinessLogic(IPersonRepository personRepository) {
            _personRepository = personRepository;
        }

        public object AgeAround(int year) => _personRepository.AgeAround(year);

        public MemoryStream ExportToExcel() => _personRepository.ExportToExcel();

        public List<string> GetFullName() => _personRepository.GetFullName();

        public List<Person> GetMales() => _personRepository.GetMales();

        public Person GetOldest() => _personRepository.GetOldest();

        public List<Person> GetPeople() => _personRepository.GetPeople();
    }
}
