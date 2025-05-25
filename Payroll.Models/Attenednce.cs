using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    [Index(nameof(GreaterThanDays), IsUnique = true)]

    public class Attendnce
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Greater-than days value is required.")]
        [Range(0, 100, ErrorMessage = "days of absence must be between 0 and 100.")]
        public int GreaterThanDays { get; set; }

        [Required(ErrorMessage = " percentage is required.")]
        [Range(0.0, 100.0, ErrorMessage = "Incentive percentage must be between 0 and 100.")]
        public decimal Percentage { get; set; }
    }
}
