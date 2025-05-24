using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using Payroll.Models.ViewModels;

namespace Payroll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly ISalaryService _salaryService;
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService
            , ISalaryService salaryService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _salaryService = salaryService;
        }

        private async Task PopulateDropdownListsAsync(EmployeeVM employeeVM)
        {
            var departments = await _departmentService.GetAllAsync();
            var salary = await _salaryService.GetAllAsync();

            employeeVM.DepartmentList = departments.Select(u => new SelectListItem
            {
                Text = u.DepartmentName,
                Value = u.Id.ToString()
            }).ToList();

            employeeVM.SalaryList = salary.Select(u => new SelectListItem
            {
                Text = u.GradeName,
                Value = u.Id.ToString()
            }).ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            var employeeVM = new EmployeeVM { Employee = new Employee
            {
                DateOfBirth = DateTime.Today,
                DateOfHire = DateTime.Today
            }
            };
            await PopulateDropdownListsAsync(employeeVM);
            if (id == null || id == 0)
            {
                return View(employeeVM);
            }
            employeeVM.Employee = await _employeeService.GetByIdAsync(id.Value);
            return View(employeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(EmployeeVM obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Employee.Id == 0)
                    await _employeeService.CreateAsync(obj.Employee);
                else
                    await _employeeService.UpdateAsync(obj.Employee);

                TempData["success"] = "Employee saved successfully";
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdownListsAsync(obj);
            return View(obj);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (!id.HasValue)
            {
                return Json(new { success = false, message = "Invalid employee id" });
            }

            var employeeToBeDeleted = await _employeeService.GetByIdAsync(id.Value);
            if (employeeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Employee not found" });
            }

            await _employeeService.DeleteAsync(employeeToBeDeleted);

            return Json(new { success = true, message = "Delete Successful" });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
var employees = await _employeeService.GetAllAsync();

            return Json(new { data = employees });

        }
    }
}
