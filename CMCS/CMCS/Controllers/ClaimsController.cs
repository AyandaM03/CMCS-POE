
using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly CMCSContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClaimsController(CMCSContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // ----------------------------------------
        // 1. Show claim submission form
        // ----------------------------------------
        [HttpGet]
        public IActionResult Submit()
        {
            return View(new Claim());
        }

        // ----------------------------------------
        // 2. Submit claim + upload document
        // ----------------------------------------
        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(claim);

            // Handle file upload
            if (file != null)
            {
                string folder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                claim.SupportingDocument = file.FileName;
            }

            claim.SubmittedDate = DateTime.Now;
            claim.Status = "Pending";

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Your claim has been submitted!";
            return RedirectToAction("Track");
        }

        // ----------------------------------------
        // 3. Track all claims
        // ----------------------------------------
        public IActionResult Track()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        // ----------------------------------------
        // 4. Change claim status (for PC + Manager)
        // ----------------------------------------
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
