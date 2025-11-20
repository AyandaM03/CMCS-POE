using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        private readonly CMCSContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<LecturerController> _logger;

        public LecturerController(CMCSContext context, IWebHostEnvironment env, ILogger<LecturerController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        //  Lecturer Claim Submission Page
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Claim());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Claim claim, IFormFile? supportingFile)
        {
            if (!ModelState.IsValid)
                return View(claim);

            try
            {
                if (supportingFile != null && supportingFile.Length > 0)
                {
                    //  Allowed file types and size validation
                    var allowed = new[] { ".pdf", ".docx", ".xlsx" };
                    var ext = Path.GetExtension(supportingFile.FileName).ToLowerInvariant();

                    if (!allowed.Contains(ext))
                    {
                        ModelState.AddModelError("SupportingDocument", "Only .pdf, .docx, and .xlsx files are allowed.");
                        return View(claim);
                    }

                    if (supportingFile.Length > 5 * 1024 * 1024) // 5 MB max
                    {
                        ModelState.AddModelError("SupportingDocument", "File too large (max 5MB).");
                        return View(claim);
                    }

                    var uploads = Path.Combine(_env.WebRootPath, "uploads");
                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    var unique = $"{Guid.NewGuid()}{ext}";
                    var filePath = Path.Combine(uploads, unique);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await supportingFile.CopyToAsync(fs);
                    }

                    // 
                    claim.SupportingDocument = unique;
                }

                // Store submission time
                claim.SubmittedDate = DateTime.Now;
                claim.Status = "Pending";

                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();

                return View("Success", claim);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting claim");
                ModelState.AddModelError("", "An error occurred while submitting the claim. Please try again.");
                return View(claim);
            }
        }

        //  Lecturer Claim Tracking Page
        [HttpGet]
        public IActionResult Track()
        {
            var claims = _context.Claims
                .OrderByDescending(c => c.SubmittedDate)
                .ToList() ?? new List<Claim>(); // ✅ ensures it's never null

            return View("~/Views/Claims/Track.cshtml", claims);
        }

    }
}

