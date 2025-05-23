using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
       IDepartmentRepository DepartmentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        ISalaryRepository SalaryRepository { get; }
        void Save();
    }
}
