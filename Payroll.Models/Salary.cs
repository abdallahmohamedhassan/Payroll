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
    public class Salary
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Grade name is required.")]
        [StringLength(50, ErrorMessage = "Grade name cannot exceed 50 characters.")]
        public string GradeName { get; set; }

        [Required(ErrorMessage = "Base salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Base salary must be a non-negative number.")]
        public decimal BaseSalary { get; set; }
        [JsonIgnore]
        [ValidateNever]

        public ICollection<Employee> Employees { get; set; }

    }
}
