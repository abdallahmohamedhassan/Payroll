using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SalaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Salary> CreateAsync(Salary salary)
        {
            await _unitOfWork.SalaryRepository.AddAsync(salary);
            await _unitOfWork.Save();
            return salary;
        }

        public async Task UpdateAsync(Salary salary)
        {


            await _unitOfWork.SalaryRepository.UpdateAsync(salary);

            await _unitOfWork.Save();
        }



        public async Task<IEnumerable<Salary>> GetAllAsync()
        {
            List<Salary> salary = (await _unitOfWork.SalaryRepository.GetAllAsync()).ToList();
            return salary;

        }
            public async Task<Salary> GetByIdAsync(int id)
            {
                var salary = await _unitOfWork.SalaryRepository.GetAsync(u => u.Id == id);
                return salary;
            }
        }
    }
