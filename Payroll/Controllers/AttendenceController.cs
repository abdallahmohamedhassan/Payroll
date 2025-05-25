using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces;
using Payroll.Models;

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
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Attendnce attenednce = new Attendnce();
            if (id == null || id == 0)
            {
                return View(attenednce);
            }
            attenednce = await _attendenceService.GetByIdAsync(id.Value);
            return View(attenednce);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(Attendnce obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                    await _attendenceService.CreateAsync(obj);
                else
                    await _attendenceService.UpdateAsync(obj);

                TempData["success"] = "Attendnce saved successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attenednce = await _attendenceService.GetAllAsync();

            return Json(new { data = attenednce });

        }
    
}
}
