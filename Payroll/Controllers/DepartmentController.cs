using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Application.Services;
using Payroll.Models.ViewModels;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController( IDepartmentService departmentService)
        {
           
            _departmentService = departmentService;
         
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Department department=new Department();
            if (id == null || id == 0)
            {
                return View(department);
            }
            department = await _departmentService.GetByIdAsync(id.Value);
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Department obj)
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
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? id)
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllAsync();

            return Json(new { data = departments });

        }
    }
}
