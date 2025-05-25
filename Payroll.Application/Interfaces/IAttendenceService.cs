
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces
{
    public interface IAttendenceService
    {
        Task<IEnumerable<Attendnce>> GetAllAsync();
        Task<Attendnce> GetByIdAsync(int id);
        Task<Attendnce> CreateAsync(Attendnce attenednce);
        Task UpdateAsync(Attendnce attenednce);
    }
}
