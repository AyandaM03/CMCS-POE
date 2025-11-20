using CMCS.Data;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCS.Controllers
{
    public class HRController : Controller
    {
        private readonly CMCSContext _context;

        public HRController(CMCSContext context)
        {
            _context = context;
        }

        // HR Dashboard: show all manager-approved claims for payroll/reporting
        public async Task<IActionResult> Index()
        {
            // Only final approved claims (manager-approved)
            var approved = await _context.Claims
                .Where(c => c.Status == "Approved_Manager" || c.Status == "Approved")
                .OrderByDescending(c => c.SubmittedDate)
                .ToListAsync();

            return View(approved);
        }

        // Generate a printable HTML invoice/report for a single claim
        // GET: /HR/Invoice/5
        public async Task<IActionResult> Invoice(int id)
        {
            var claim = await _context.Claims
                .FirstOrDefaultAsync(c => c.ClaimId == id);

            if (claim == null)
                return NotFound();

            return View(claim);
        }

        // Optional: bulk report (simple HTML view listing multiple claims)
        public async Task<IActionResult> BulkReport()
        {
            var approved = await _context.Claims
                .Where(c => c.Status == "Approved_Manager" || c.Status == "Approved")
                .OrderByDescending(c => c.SubmittedDate)
                .ToListAsync();

            return View("BulkReport", approved);
        }
    }
}

