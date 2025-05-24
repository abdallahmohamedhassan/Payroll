using Payroll.DataAccess.Data;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DataAccess.Repository
{
    public class ExperienceIncentiveRepository : Repository<ExperienceIncentive>, IExperienceIncentiveRepository
    {
        public ExperienceIncentiveRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
