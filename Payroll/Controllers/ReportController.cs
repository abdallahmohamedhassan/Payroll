using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Application.Interfaces;
using Payroll.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class ReportController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAttendenceService _attendenceService;
        private readonly IReportService _reportService;

        public ReportController(IEmployeeService employeeService, IAttendenceService attendenceService, IReportService reportService)
        {
            _employeeService = employeeService;
            _attendenceService = attendenceService;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ReportVM reportVM = new ReportVM();

                var employees = await _employeeService.GetAllAsync();
                var attendence = await _attendenceService.GetAllAsync();

                reportVM.EmployeeList = employees.Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id.ToString()
                }).ToList();

                reportVM.AttendenceList = attendence.Select(u => new SelectListItem
                {
                    Text = u.GreaterThanDays.ToString(),
                    Value = u.Id.ToString()
                }).ToList();

                return View(reportVM);
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "Failed to load report data.";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CalculateSalaryAsync(int employeeId, int attendenceId)
        {
            try
            {
                var employeeSalary = await _reportService.CalculateEmployeeSalary(employeeId, attendenceId);
                return PartialView("_EmployeeSalaryList", employeeSalary);
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "Failed to calculate salary.";
                // Return an empty partial or an error partial depending on your UI
                return PartialView("_EmployeeSalaryList", null);
            }
        }
    }
}
