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

        public async Task<IActionResult> Index()
        {
            var lecturers = await _context.Lecturers.ToListAsync();
            return View(lecturers);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return NotFound();

            return View(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Lecturer lecturer)
        {
            if (!ModelState.IsValid)
                return View(lecturer);

            // Ensure role is valid
            if (lecturer.RoleID == 0)
                lecturer.RoleID = 1;

            _context.Update(lecturer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            // Ensure role is valid
            lecturer.RoleID = 1;

            _context.Add(lecturer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
