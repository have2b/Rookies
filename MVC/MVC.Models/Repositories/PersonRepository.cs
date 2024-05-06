using System.Globalization;
using CsvHelper;
using MVC.Models.Models;
using OfficeOpenXml;

namespace MVC.Models.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly List<Person> _people;

        public PersonRepository()
        {
            _people = LoadDataFromCsv();
        }

        private static List<Person> LoadDataFromCsv()
        {
            try
            {
                using var reader = new StreamReader("../MVC.Models/Data/Person_data.csv");
                using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                return csvReader.GetRecords<Person>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadDataFromCsv: {ex.Message}");
                return [];
            }
        }

        public object AgeAround(int year)
        {
            var inYear = _people.Where(p => p.DateOfBirth.Year == year).ToList();
            var beforeYear = _people.Where(p => p.DateOfBirth.Year < year).ToList();
            var afterYear = _people.Where(p => p.DateOfBirth.Year > year).ToList();

            return new
            {
                Inyear = inYear,
                BeforeYear = beforeYear,
                AfterYear = afterYear
            };
        }

        public MemoryStream ExportToExcel()
        {
            var stream = new MemoryStream();
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets.Add("People");
            worksheet.Cells.LoadFromCollection(_people, true);

            // Change format of date column
            worksheet.Column(4).Style.Numberformat.Format = "dd/mm/yyyy";
            package.Save();

            stream.Position = 0;
            return stream;
        }

        public List<string> GetFullName() =>
            _people.Select(p => $"{p.FirstName} {p.LastName}").ToList();

        public List<Person> GetMales() => _people.Where(p => p.Gender == GenderType.Male).ToList();

        public Person GetOldest() => _people.OrderBy(p => p.DateOfBirth.Year).FirstOrDefault();

        public List<Person> GetPeople() => _people;
    }
}
