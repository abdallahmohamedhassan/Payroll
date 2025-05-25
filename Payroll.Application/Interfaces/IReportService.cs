using Payroll.Models;
using Payroll.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces
{
    public interface IReportService
    {
        Task<EmployeeSalaryVM> CalculateEmployeeSalary(int employeeId, int attendenceId);

    }
}
