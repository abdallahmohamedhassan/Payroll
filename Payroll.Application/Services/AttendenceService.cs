using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class AttendenceService : IAttendenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Attendnce> CreateAsync(Attendnce attenednce)
        {
            try
            {
                await _unitOfWork.AttendenceRepository.AddAsync(attenednce);
                await _unitOfWork.Save();
                return attenednce;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw; // Optionally rethrow or handle as needed
            }
        }

        public async Task UpdateAsync(Attendnce attenednce)
        {
            try
            {
                await _unitOfWork.AttendenceRepository.UpdateAsync(attenednce);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<IEnumerable<Attendnce>> GetAllAsync()
        {
            try
            {
                List<Attendnce> attenednce = (await _unitOfWork.AttendenceRepository.GetAllAsync()).ToList();
                return attenednce;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<Attendnce> GetByIdAsync(int id)
        {
            try
            {
                var attenednce = await _unitOfWork.AttendenceRepository.GetAsync(u => u.Id == id);
                return attenednce;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }
    }
}
