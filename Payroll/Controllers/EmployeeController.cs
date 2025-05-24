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


        public async Task<IActionResult> Index()
        {
          var employees=  await _employeeService.GetAllAsync();
            return View(employees);
        }
        public async Task<IActionResult> UpsertAsync(int? id)
        {
            var departments = await _departmentService.GetAllAsync();
            var salary = await _salaryService.GetAllAsync();


            EmployeeVM employeeVM = new()
            {
                DepartmentList =  departments.Select(u => new SelectListItem
                {
                    Text = u.DepartmentName,  
                    Value = u.Id.ToString()
                }).ToList(),
                SalaryList = salary.Select(u => new SelectListItem
                {
                    Text = u.GradeName,  
                    Value = u.Id.ToString()
                }).ToList(),
                Employee = new Employee()
            };
            if (id == null || id == 0)
            {
                return View(employeeVM);
            }
            else
            {
                employeeVM.Employee = await _employeeService.GetByIdAsync(id.Value);
                return View(employeeVM);

            }
        }
        [HttpPost]
        public async Task<IActionResult> UpsertAsync(EmployeeVM obj)
        {

            if (ModelState.IsValid)
            {
               
                if (obj.Employee.Id == 0)
                {
                    await _employeeService.CreateAsync(obj.Employee);
                }
                else
                {
                    await _employeeService.UpdateAsync(obj.Employee);

                }
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            else

            {
                var departments = await _departmentService.GetAllAsync();
                var salary = await _salaryService.GetAllAsync();
              obj.  DepartmentList = departments.Select(u => new SelectListItem
                {
                    Text = u.DepartmentName,
                    Value = u.Id.ToString()
                }).ToList();
                obj.SalaryList = salary.Select(u => new SelectListItem
                {
                    Text = u.GradeName,
                    Value = u.Id.ToString()
                }).ToList();
                return View(obj);

            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            var employeeToBeDeleted = await _employeeService.GetByIdAsync(id.Value);
            if (employeeToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }




            await _employeeService.DeleteAsync(employeeToBeDeleted);
            

            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
