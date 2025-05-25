using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Models;
using System;
using System.Threading.Tasks;

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
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "An error occurred while loading the page.";
                return View("Error");
            }
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                ExperienceIncentive experienceIncentive = new ExperienceIncentive();
                if (id == null || id == 0)
                {
                    return View(experienceIncentive);
                }
                experienceIncentive = await _experienceIncentiveService.GetByIdAsync(id.Value);
                return View(experienceIncentive);
            }
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "Failed to load experience incentive.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(ExperienceIncentive obj)
        {
            try
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
            catch (Exception ex)
            {
                // Log error here
                TempData["error"] = "An error occurred while saving experience incentive.";
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var experienceIncentive = await _experienceIncentiveService.GetAllAsync();
                return Json(new { data = experienceIncentive });
            }
            catch (Exception ex)
            {
                // Log error here
                return Json(new { success = false, message = "Failed to retrieve data." });
            }
        }
    }
}
