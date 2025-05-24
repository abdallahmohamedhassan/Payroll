using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces
{
    public interface IExperienceIncentiveService
    {
        Task<IEnumerable<ExperienceIncentive>> GetAllAsync();
        Task<ExperienceIncentive> GetByIdAsync(int id);
        Task<ExperienceIncentive> CreateAsync(ExperienceIncentive experienceIncentive);
        Task UpdateAsync(ExperienceIncentive experienceIncentive);
    }
}
