using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }
        [Range(0.0, 100.0, ErrorMessage = "Incentive percentage must be between 0 and 100.")]
        public decimal IncentivePercentage { get; set; }

        [JsonIgnore]
        [ValidateNever]

        public ICollection<Employee> Employees { get; set; }

    }
}
