using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Application.Services;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService _salaryService;
        public SalaryController( ISalaryService salaryService)
        {
          
            _salaryService = salaryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Salary salary = new Salary();
            if (id == null || id == 0)
            {
                return View(salary);
            }
            salary = await _salaryService.GetByIdAsync(id.Value);
            return View(salary);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Salary obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                    await _salaryService.CreateAsync(obj);
                else
                    await _salaryService.UpdateAsync(obj);

                TempData["success"] = "Salary saved successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _salaryService.GetAllAsync();

            return Json(new { data = departments });

        }
    }
}
