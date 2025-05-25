using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models.ViewModels
{
    public class EmployeeSalaryVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int YearOfExperience { get; set; }
        public string DepartmentName { get; set; }
        public string GradeName { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal DepartmenIncentive { get; set; }
        public decimal ExperienceIncentive { get; set; }
        public decimal AttendenceIncentive { get; set; }
        public decimal ToTalSalary { get; set; }


    }
}
