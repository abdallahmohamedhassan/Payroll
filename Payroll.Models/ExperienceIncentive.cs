using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    [Index(nameof(GreaterThanYear), IsUnique = true)]

    public class ExperienceIncentive 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Greater-than year value is required.")]
        [Range(0, 100, ErrorMessage = "Years of experience must be between 0 and 100.")]
        public int GreaterThanYear { get; set; }

        [Required(ErrorMessage = "Incentive percentage is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Incentive percentage must be between 0 and 100.")]
        public decimal IncentivePercentage { get; set; }

        
    }
}
