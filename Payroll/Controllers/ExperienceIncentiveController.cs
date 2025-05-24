using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Application.Services;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class ExperienceIncentiveController : Controller
    {
        private readonly IExperienceIncentiveService _experienceIncentiveService;
        public ExperienceIncentiveController(IExperienceIncentiveService experienceIncentiveService)
        {

            _experienceIncentiveService = experienceIncentiveService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            ExperienceIncentive experienceIncentive = new ExperienceIncentive();
            if (id == null || id == 0)
            {
                return View(experienceIncentive);
            }
            experienceIncentive = await _experienceIncentiveService.GetByIdAsync(id.Value);
            return View(experienceIncentive);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(ExperienceIncentive obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                    await _experienceIncentiveService.CreateAsync(obj);
                else
                    await _experienceIncentiveService.UpdateAsync(obj);

                TempData["success"] = "Experience Incentive saved successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var experienceIncentive = await _experienceIncentiveService.GetAllAsync();

            return Json(new { data = experienceIncentive });

        }
    }
}
