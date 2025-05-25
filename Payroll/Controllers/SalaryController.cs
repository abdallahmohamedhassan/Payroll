using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Models;
using System;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "An error occurred while loading the page.";
                return View("Error");
            }
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                Salary salary = new Salary();
                if (id == null || id == 0)
                {
                    return View(salary);
                }

                salary = await _salaryService.GetByIdAsync(id.Value);
                return View(salary);
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "Failed to load salary data.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Salary obj)
        {
            try
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
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "An error occurred while saving salary.";
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var salaries = await _salaryService.GetAllAsync();
                return Json(new { data = salaries });
            }
            catch (Exception ex)
            {
                // Log error here
                return Json(new { success = false, message = "Failed to retrieve salary data." });
            }
        }
    }
}
