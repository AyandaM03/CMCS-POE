using CMCS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly CMCSContext _context;

        public CoordinatorController(CMCSContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View("~/Views/Claims/PreApprove.cshtml", claims);
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

