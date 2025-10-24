using CMCS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly CMCSContext _context;
        public CoordinatorController(CMCSContext context) => _context = context;

        public IActionResult Index()
        {
            var claims = _context.Claims.OrderByDescending(c => c.SubmittedDate).ToList();
            // Both PC and PM see same table; coordinator sees buttons for pre-approve/reject
            ViewData["Role"] = "Coordinator";
            return View("~/Views/Claims/Review.cshtml", claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string actionType)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null) return NotFound();

            if (actionType == "PreApprove")
                claim.Status = "PreApproved";
            else if (actionType == "Reject")
                claim.Status = "Rejected";

            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

