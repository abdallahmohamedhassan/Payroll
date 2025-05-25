using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log the error (ex)
                TempData["error"] = "An error occurred while loading the page.";
                return View("Error");
            }
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                Department department = new Department();
                if (id == null || id == 0)
                {
                    return View(department);
                }
                department = await _departmentService.GetByIdAsync(id.Value);
                return View(department);
            }
            catch (Exception ex)
            {
                // Log error
                TempData["error"] = "An error occurred while loading the department.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Department obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.Id == 0)
                        await _departmentService.CreateAsync(obj);
                    else
                        await _departmentService.UpdateAsync(obj);

                    TempData["success"] = "Department saved successfully";
                    return RedirectToAction(nameof(Index));
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                // Log error
                TempData["error"] = "An error occurred while saving the department.";
                return View(obj);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return Json(new { success = false, message = "Invalid Department id" });
                }

                var departmentToBeDeleted = await _departmentService.GetByIdAsync(id.Value);
                if (departmentToBeDeleted == null)
                {
                    return Json(new { success = false, message = "Department not found" });
                }

                await _departmentService.DeleteAsync(departmentToBeDeleted);

                return Json(new { success = true, message = "Delete Successful" });
            }
            catch (Exception ex)
            {
                // Log error
                return Json(new { success = false, message = "Error occurred during delete operation." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var departments = await _departmentService.GetAllAsync();
                return Json(new { data = departments });
            }
            catch (Exception ex)
            {
                // Log error
                return Json(new { success = false, message = "Failed to retrieve departments." });
            }
        }
    }
}
