using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            try
            {
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                await _unitOfWork.Save();
                return employee;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task DeleteAsync(Employee employee)
        {
            try
            {
                await _unitOfWork.EmployeeRepository.RemoveAsync(employee);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            try
            {
                List<Employee> employee = (await _unitOfWork.EmployeeRepository.GetAllAsync(includeProperties: "Salary,Department")).ToList();
                return employee;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            try
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(u => u.Id == id);
                return employee;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }
    }
}
