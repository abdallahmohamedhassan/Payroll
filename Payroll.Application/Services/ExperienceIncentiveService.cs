using Payroll.Application.Interfaces;
using Payroll.DataAccess.Repository.IRepository;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Services
{
    public class ExperienceIncentiveService:IExperienceIncentiveService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExperienceIncentiveService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExperienceIncentive> CreateAsync(ExperienceIncentive experienceIncentive)
        {
           
            await _unitOfWork.ExperienceIncentiveRepository.AddAsync(experienceIncentive);
            await _unitOfWork.Save();
            return experienceIncentive;
        }

        public async Task UpdateAsync(ExperienceIncentive experienceIncentive)
        {
            

            await _unitOfWork.ExperienceIncentiveRepository.UpdateAsync(experienceIncentive);

            await _unitOfWork.Save();
        }

     
        public async Task<IEnumerable<ExperienceIncentive>> GetAllAsync()
        {
            List<ExperienceIncentive> experienceIncentive = (await _unitOfWork.ExperienceIncentiveRepository.GetAllAsync()).ToList();
            return experienceIncentive;
        }

        public async Task<ExperienceIncentive> GetByIdAsync(int id)
        {
            var experienceIncentive = await _unitOfWork.ExperienceIncentiveRepository.GetAsync(u => u.Id == id);
            return experienceIncentive;
        }
    }
}
