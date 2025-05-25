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
    public class AttendenceService : IAttendenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Attendnce> CreateAsync(Attendnce attenednce)
        {
            await _unitOfWork.AttendenceRepository.AddAsync(attenednce);
            await _unitOfWork.Save();
            return attenednce;
        }

        public async Task UpdateAsync(Attendnce attenednce)
        {


            await _unitOfWork.AttendenceRepository.UpdateAsync(attenednce);

            await _unitOfWork.Save();
        }



        public async Task<IEnumerable<Attendnce>> GetAllAsync()
        {
            List<Attendnce> attenednce = (await _unitOfWork.AttendenceRepository.GetAllAsync()).ToList();
            return attenednce;

        }
        public async Task<Attendnce> GetByIdAsync(int id)
        {
            var attenednce = await _unitOfWork.AttendenceRepository.GetAsync(u => u.Id == id);
            return attenednce;
        }
    }
}
