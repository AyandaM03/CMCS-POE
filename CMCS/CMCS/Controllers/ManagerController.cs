using CMCS.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
    {
        private readonly CMCSContext _context;
        public ManagerController(CMCSContext context) => _context = context;

        public IActionResult Index()
        {
            var claims = _context.Claims.OrderByDescending(c => c.SubmittedDate).ToList();
            ViewData["Role"] = "Manager";
            return View("~/Views/Claims/Review.cshtml", claims);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string actionType)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null) return NotFound();

            if (actionType == "Approve")
                claim.Status = "Approved";
            else if (actionType == "Reject")
                claim.Status = "Rejected";
            else if (actionType == "Settle")
                claim.Status = "Settled"; // optional

            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}



