using Bulky.DataAccess.Repository.IRepository;
using Payroll.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _db;
       public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            
         }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
