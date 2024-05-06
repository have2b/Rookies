using MVC.Models.Models;

namespace MVC.BusinessLogic
{
    public interface IPersonBusinessLogic
    {
        List<Person> GetPeople();
        List<Person> GetMales();
        Person GetOldest();
        List<string> GetFullName();
        object AgeAround(int year);
        MemoryStream ExportToExcel();
    }
}
