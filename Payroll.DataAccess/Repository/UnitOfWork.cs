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
     public   ISalaryRepository _salaryRepository { get; private set; }
        public IEmployeeRepository _employeeRepository { get; private set; }
        public IDepartmentRepository _departmentRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            _salaryRepository= new SalaryRepository(_db);
            _employeeRepository= new EmployeeRepository(_db);
            _departmentRepository=new DepartmentRepository(_db);

         }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
