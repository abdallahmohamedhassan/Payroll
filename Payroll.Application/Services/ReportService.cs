using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using Payroll.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeSalaryVM> CalculateEmployeeSalary(int employeeId, int attendenceId)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(u => u.Id == employeeId, includeProperties: "Salary,Department");
                var attendence = await _unitOfWork.AttendenceRepository.GetAsync(u => u.Id == attendenceId);

                if (employee == null)
                    throw new Exception($"Employee with ID {employeeId} not found.");
                if (attendence == null)
                    throw new Exception($"Attendance with ID {attendenceId} not found.");

                EmployeeSalaryVM employeeSalaryVM = new EmployeeSalaryVM
                {
                    FullName = employee.FullName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    YearOfExperience = DateTime.Today.Year - employee.DateOfHire.Year,
                    DepartmentName = employee.Department?.DepartmentName,
                    GradeName = employee.Salary?.GradeName,
                    BaseSalary = employee.Salary?.BaseSalary ?? 0,
                    DepartmenIncentive = (employee.Salary?.BaseSalary ?? 0) * ((employee.Department?.IncentivePercentage ?? 0) / 100)
                };

                var experienceSalary = (await _unitOfWork.ExperienceIncentiveRepository.GetAllAsync())
                    .OrderBy(e => e.GreaterThanYear)
                    .ToList();

                var experienceincentive = experienceSalary
                    .Where(x => x.GreaterThanYear < employeeSalaryVM.YearOfExperience)
                    .FirstOrDefault();

                employeeSalaryVM.ExperienceIncentive = experienceincentive != null
                    ? employeeSalaryVM.BaseSalary * (experienceincentive.IncentivePercentage / 100)
                    : 0;

                if (attendence.GreaterThanDays <= 1)
                {
                    employeeSalaryVM.AttendenceIncentive = employeeSalaryVM.BaseSalary * (attendence.Percentage / 100);
                }
                else
                {
                    employeeSalaryVM.AttendenceIncentive = (employeeSalaryVM.BaseSalary * (attendence.Percentage / 100)) * -1;
                }

                employeeSalaryVM.ToTalSalary = employeeSalaryVM.AttendenceIncentive
                    + employeeSalaryVM.ExperienceIncentive
                    + employeeSalaryVM.DepartmenIncentive
                    + employeeSalaryVM.BaseSalary;

                return employeeSalaryVM;
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using ILogger)
                throw;  // Optionally rethrow or handle accordingly
            }
        }
    }
}
