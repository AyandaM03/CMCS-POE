using Microsoft.AspNetCore.Mvc;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult Index()

        {
  
            var claims = new List<Claim>
            {
                new Claim
                {
                    ClaimId = 101,
                    LecturerName = "Bulelani Mdolo",
                    HoursWorked = 12,
                    HourlyRate = 250,
                    Status = "Pending"
                },
                new Claim
                {
                    ClaimId = 102,
                    LecturerName = "Ayanda Mbana",
                    HoursWorked = 8,
                    HourlyRate = 300,
                    Status = "Pending"
                }
            };

            return View(claims);
        }
    }
}

