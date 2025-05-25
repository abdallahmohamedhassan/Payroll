using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                await _unitOfWork.SalaryRepository.AddAsync(salary);
                await _unitOfWork.Save();
                return salary;
            }
            catch (Exception ex)
            {
                // You can log the exception here or rethrow
                throw;
            }
        }

        public async Task UpdateAsync(Salary salary)
        {
            try
            {
                await _unitOfWork.SalaryRepository.UpdateAsync(salary);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw;
            }
        }

        public async Task<IEnumerable<Salary>> GetAllAsync()
        {
            try
            {
                var salaryList = (await _unitOfWork.SalaryRepository.GetAllAsync()).ToList();
                return salaryList;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw ;
            }
        }

        public async Task<Salary> GetByIdAsync(int id)
        {
            try
            {
                var salary = await _unitOfWork.SalaryRepository.GetAsync(u => u.Id == id);
                return salary;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                throw ;
            }
        }
    }
}
