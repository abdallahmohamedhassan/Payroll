using Payroll.DataAccess.Data;
using Payroll.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _db;
    

        public IDepartmentRepository DepartmentRepository { get; private set; }

        public IEmployeeRepository EmployeeRepository { get; private set; }

        public ISalaryRepository SalaryRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            SalaryRepository = new SalaryRepository(_db);
            EmployeeRepository = new EmployeeRepository(_db);
            DepartmentRepository = new DepartmentRepository(_db);

         }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
