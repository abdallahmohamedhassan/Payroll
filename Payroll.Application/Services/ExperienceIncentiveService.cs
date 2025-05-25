using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class ExperienceIncentiveService : IExperienceIncentiveService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExperienceIncentiveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExperienceIncentive> CreateAsync(ExperienceIncentive experienceIncentive)
        {
            try
            {
                await _unitOfWork.ExperienceIncentiveRepository.AddAsync(experienceIncentive);
                await _unitOfWork.Save();
                return experienceIncentive;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task UpdateAsync(ExperienceIncentive experienceIncentive)
        {
            try
            {
                await _unitOfWork.ExperienceIncentiveRepository.UpdateAsync(experienceIncentive);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<IEnumerable<ExperienceIncentive>> GetAllAsync()
        {
            try
            {
                List<ExperienceIncentive> experienceIncentive = (await _unitOfWork.ExperienceIncentiveRepository.GetAllAsync()).ToList();
                return experienceIncentive;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }

        public async Task<ExperienceIncentive> GetByIdAsync(int id)
        {
            try
            {
                var experienceIncentive = await _unitOfWork.ExperienceIncentiveRepository.GetAsync(u => u.Id == id);
                return experienceIncentive;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw;
            }
        }
    }
}
