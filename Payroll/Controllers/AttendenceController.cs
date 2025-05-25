using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Models;
using System; // for Exception

namespace Payroll.Controllers
{
    public class AttendenceController : Controller
    {
        private readonly IAttendenceService _attendenceService;
        public AttendenceController(IAttendenceService attendenceService)
        {
            _attendenceService = attendenceService;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                // Log the error (ex) here
                TempData["error"] = "An error occurred while loading the page.";
                return View("Error"); // or return some error view
            }
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                Attendnce attenednce = new Attendnce();
                if (id == null || id == 0)
                {
                    return View(attenednce);
                }
                attenednce = await _attendenceService.GetByIdAsync(id.Value);
                return View(attenednce);
            }
            catch (Exception ex)
            {
                // Log the error (ex) here
                TempData["error"] = "An error occurred while loading attendance data.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Attendnce obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.Id == 0)
                        await _attendenceService.CreateAsync(obj);
                    else
                        await _attendenceService.UpdateAsync(obj);

                    TempData["success"] = "Attendance saved successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                // Log the error (ex) here
                TempData["error"] = "An error occurred while saving attendance.";
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var attenednce = await _attendenceService.GetAllAsync();
                return Json(new { data = attenednce });
            }
            catch (Exception ex)
            {
                // Log the error (ex) here
                return Json(new { success = false, message = "Failed to retrieve attendance data." });
            }
        }
    }
}
