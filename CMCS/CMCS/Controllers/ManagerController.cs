using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
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
                    Status = "Approved"
                },
                new Claim
                {
                    ClaimId = 102,
                    LecturerName = "Ayanda Mbana",
                    HoursWorked = 8,
                    HourlyRate = 300,
                    Status = "Rejected"
                }
            };

            return View(claims);
        }
    }
}


