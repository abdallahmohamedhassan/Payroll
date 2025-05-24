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
    public  class DepartmentService:IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await _unitOfWork.DepartmentRepository.AddAsync(department);
            await _unitOfWork.Save();
            return department;
        }

        public async Task UpdateAsync(Department department)
        {


            await _unitOfWork.DepartmentRepository.UpdateAsync(department);

            await _unitOfWork.Save();
        }

        public async Task DeleteAsync(Department department)
        {


            await _unitOfWork.DepartmentRepository.RemoveAsync(department);
            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            List<Department> department = (await _unitOfWork.DepartmentRepository.GetAllAsync()).ToList();
            return department;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(u => u.Id == id);
            return department;
        }

    }
}
