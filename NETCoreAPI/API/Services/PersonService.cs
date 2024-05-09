using System.Globalization;
using API.Interfaces;
using API.DTOs;
using API.Models;
using CsvHelper;

namespace API.Services
{
    public class PersonService : IPersonService
    {
        private static readonly List<Person> _people = LoadFromCsv();
        private static ILogger _logger;

        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        private static List<Person> LoadFromCsv()
        {
            try
            {
                using var reader = new StreamReader("./Data/MOCK_DATA.csv");
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csv.GetRecords<Person>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading data from CSV");
                return [];
            }
        }

        public Person? Add(PersonDTO personDTO)
        {
            try
            {
                var person = new Person()
                {
                    Id = Guid.NewGuid(),
                    FirstName = personDTO.FirstName,
                    LastName = personDTO.LastName,
                    Gender = personDTO.Gender,
                    DateOfBirth = personDTO.DateOfBirth,
                    BirthPlace = personDTO.BirthPlace,
                };
                _people.Add(person);
                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding person");
                return null;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var person = _people.FirstOrDefault(p => p.Id == id);
                if (person is null)
                {
                    return false;
                }
                _people.Remove(person);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting person");
                return false;
            }
        }

        public List<Person> GetAll(string? name, GenderType? gender, string? birthPlace)
        {
            try
            {
                var people = _people.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    people = people.Where(p => p.FullName.Contains(name));
                }

                if (gender != null)
                {
                    people = people.Where(p => p.Gender == gender);
                }

                if (!string.IsNullOrEmpty(birthPlace))
                {
                    people = people.Where(p => p.BirthPlace.Contains(birthPlace));
                }

                return [.. people];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all people");
                return [];
            }
        }

        public Person? Update(Guid id, PersonDTO personDTO)
        {
            try
            {
                var person = _people.FirstOrDefault(p => p.Id == id);
                if (person == null)
                {
                    return null;
                }
                person.FirstName = personDTO.FirstName;
                person.LastName = personDTO.LastName;
                person.Gender = personDTO.Gender;
                person.DateOfBirth = personDTO.DateOfBirth;
                person.BirthPlace = personDTO.BirthPlace;

                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating person");
                return null;
            }
        }
    }
}
