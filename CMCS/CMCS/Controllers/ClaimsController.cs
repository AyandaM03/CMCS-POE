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

        [HttpGet]
        public IActionResult Submit()
        {
            return View(new Claim());
        }

        [HttpPost]
        public async Task<IActionResult> Submit(Claim claim, IFormFile? file)
        {
            if (file != null)
            {
                string folder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(folder);

                string path = Path.Combine(folder, file.FileName);

                using var fs = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(fs);

                claim.SupportingDocument = file.FileName;
            }

            claim.Status = "Pending";
            claim.SubmittedDate = DateTime.Now;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Claim successfully submitted!";
            return RedirectToAction("Track");
        }

        public IActionResult Track()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var claim = _context.Claims.FirstOrDefault(x => x.ClaimId == id);
            if (claim == null) return NotFound();

            claim.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Track");
        }
    }
}

