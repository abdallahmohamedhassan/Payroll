using Microsoft.EntityFrameworkCore;
using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{

    public class EmployeeService:IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _unitOfWork.EmployeeRepository.AddAsync(employee);
            await _unitOfWork.Save();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
         

            await _unitOfWork.EmployeeRepository.UpdateAsync(employee);

                await _unitOfWork.Save();
        }

        public async Task DeleteAsync(Employee employee)
        {
            

             await _unitOfWork.EmployeeRepository.RemoveAsync(employee);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            List<Employee> employee = (await _unitOfWork.EmployeeRepository.GetAllAsync(includeProperties: "Salary,Department")).ToList();
            return employee;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(u => u.Id == id);
            return employee;
        }

     
    }
}
