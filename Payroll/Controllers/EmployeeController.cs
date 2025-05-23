using Microsoft.AspNetCore.Mvc;

namespace Payroll.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
