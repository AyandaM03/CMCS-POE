
using Microsoft.AspNetCore.Mvc;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Claim claim)
        {
            if (ModelState.IsValid)
            {
            
                return View("Success", claim);
            }

            return View(claim);
        }
    }
}
