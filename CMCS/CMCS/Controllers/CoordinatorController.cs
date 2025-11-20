using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;
using System.Linq;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly CMCSContext _context;

        public CoordinatorController(CMCSContext context)
        {
            _context = context;
        }

        // -----------------------------------------------------
        // LOAD PRE-APPROVAL PAGE
        // -----------------------------------------------------
        public IActionResult Index()
        {
            var claims = _context.Claims
                .Where(c => c.Status == "Pending" )
                .OrderBy(c => c.Status)
                .ToList();

            return View("PreApprove", claims);
        }

        // -----------------------------------------------------
        // COORDINATOR APPROVES (moves claim to Manager)
        // -----------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PreApprove(int id)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);

            if (claim == null)
                return NotFound();

            claim.Status = "Pre-Approved";     // Coordinator approval
            _context.SaveChanges();

            TempData["Message"] = "Claim has been approved and moved to the Academic Manager.";
            return RedirectToAction("Index");
        }

        // -----------------------------------------------------
        // COORDINATOR REJECTS
        // -----------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int id, string? reason)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);

            if (claim == null)
                return NotFound();

            claim.Status = "Rejected";

            if (!string.IsNullOrWhiteSpace(reason))
                claim.Notes += $"\n\n[Coordinator Rejection Reason]: {reason}";

            _context.SaveChanges();

            TempData["Message"] = "Claim has been rejected.";
            return RedirectToAction("Index");
        }
    }
}

