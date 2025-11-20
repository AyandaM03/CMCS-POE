using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Controllers
{
    public class LecturerManagementController : Controller
    {
        private readonly CMCSContext _context;

        public LecturerManagementController(CMCSContext context)
        {
            _context = context;
        }

        // List lecturers
        public async Task<IActionResult> Index()
        {
            var list = await _context.Lecturers
                .OrderBy(l => l.Name)
                .ToListAsync();

            return View("~/Views/LecturerManagement/Index.cshtml");

        }

        // GET: Edit lecturer
        public async Task<IActionResult> Edit(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();
            return View("~/Views/LecturerManagement/Index.cshtml");

        }

        // POST: Edit lecturer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lecturer lecturer)
        {
            if (id != lecturer.LecturerID) return BadRequest();

            if (!ModelState.IsValid)
                return View("~/Views/LecturerManagement/Index.cshtml");


            try
            {
                _context.Update(lecturer);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Lecturer updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Lecturers.Any(e => e.LecturerID == id))
                    return NotFound();
                throw;
            }
        }
    }
}

