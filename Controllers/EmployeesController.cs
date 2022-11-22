using Microsoft.AspNetCore.Mvc;
using Sqli.Services;

namespace Sqli.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string searchTermFromUser)
        {
            var employees = this.employeeService.GetByName(searchTermFromUser);
            return View(employees);
        }
    }
}