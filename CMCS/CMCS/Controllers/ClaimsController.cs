using CMCS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CMCS.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly CMCSContext _context;

        public ClaimsController(CMCSContext context)
        {
            _context = context;
        }

        public IActionResult Track()
        {
            // Fetch all claims from the database, ordered newest first
            var claims = _context.Claims
                .OrderByDescending(c => c.SubmittedDate)
                .ToList();

            // Ensure we never return a null model
            if (claims == null)
            {
                claims = new List<Models.Claim>();
            }

            return View(claims); // Pass list to Track.cshtml
        }
    }
}


