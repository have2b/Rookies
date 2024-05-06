using Microsoft.AspNetCore.Mvc;
using MVC.BusinessLogic;

namespace MVC.WebApp.Controllers
{
    [Route("NashTech/[controller]/[action]")]
    public class RookiesController : BaseController
    {
        private readonly IPersonBusinessLogic _personBusinessLogic;

        public RookiesController(IPersonBusinessLogic personBusinessLogic)
        {
            _personBusinessLogic = personBusinessLogic;
        }

        public IActionResult Index()
        {
            var people = _personBusinessLogic.GetPeople();

            return people is null ? NotFound() : Ok(people);
        }

        public IActionResult Males()
        {
            var males = _personBusinessLogic.GetMales();

            return males is null ? NotFound() : Ok(_personBusinessLogic.GetMales());
        }

        public IActionResult AgeAround([FromQuery] int year) =>
            Ok(_personBusinessLogic.AgeAround(year));

        public IActionResult Oldest()
        {
            var oldest = _personBusinessLogic.GetOldest();
            return oldest is null ? NotFound() : Ok(oldest);
        }

        public IActionResult FullName()
        {
            var fullNameList = _personBusinessLogic.GetFullName();
            return fullNameList is null ? NotFound() : Ok(fullNameList);
        }

        public IActionResult ExportToExcel()
        {
            var stream = _personBusinessLogic.ExportToExcel();
            var excelFileName = $"People_{DateTime.Now:ddmmyyyy}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelFileName);
        }
    }
}
