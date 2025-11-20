using CMCS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly CMCSContext _context;

        public ManagerController(CMCSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var claims = _context.Claims
                                .Where(c => c.Status == "Pre-Approved")
                                .ToList();

            return View("~/Views/Claims/Approve.cshtml", claims);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null) return NotFound();

            claim.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}


