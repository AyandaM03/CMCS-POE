using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly CMCSContext _context;
        private readonly IWebHostEnvironment _env;

        public ClaimsController(CMCSContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // -------------------------
        // Submit (GET)
        // -------------------------
        [HttpGet]
        public IActionResult Submit()
        {
            return View(new Claim());
        }

        // -------------------------
        // Submit (POST)
        // -------------------------
        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(claim);

            // File upload
            if (file != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(folder);

                string path = Path.Combine(folder, file.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

                claim.SupportingDocument = file.FileName;
            }

            // Auto-set
            claim.SubmittedDate = DateTime.Now;
            claim.Status = "Pending";

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Claim submitted successfully!";
            return RedirectToAction("Track");
        }

        // -------------------------
        // Lecturer Track
        // -------------------------
        public IActionResult Track()
        {
            var list = _context.Claims.OrderByDescending(c => c.SubmittedDate).ToList();
            return View(list);
        }

        // -------------------------
        // Update Status
        // -------------------------
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null)
                return NotFound();

            claim.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction("Track");
        }
    }
}
