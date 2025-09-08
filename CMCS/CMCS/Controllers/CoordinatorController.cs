using Microsoft.AspNetCore.Mvc;
using CMCS.Models; // so we can use our models

namespace CMCS.Controllers
{
    public class CoordinatorController : Controller
    {
        public IActionResult Index()
        {
            //Fake Data
            var claims = new List<Claim>
            {
                new Claim { ClaimID = 101, HoursWorked = 12, HourlyRate = 250, Status = "Pending", Lecturer = new Lecturer { Name = "Bulelani Mdolo" } },
                new Claim { ClaimID = 102, HoursWorked = 8, HourlyRate = 300, Status = "Pending", Lecturer = new Lecturer { Name = "Ayanda Mbana" } }
            };

            return View(claims); 
        }
    }
}
