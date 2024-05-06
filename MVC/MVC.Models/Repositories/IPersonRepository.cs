using MVC.Models.Models;

namespace MVC.Models.Repositories
{
    public interface IPersonRepository
    {
        List<Person> GetPeople();
        List<Person> GetMales();
        Person GetOldest();
        List<string> GetFullName();
        object AgeAround(int year);
        MemoryStream ExportToExcel();
    }
}
