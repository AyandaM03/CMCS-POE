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
                new Claim { ClaimID = 101, HoursWorked = 12, HourlyRate = 250, Status = "Approved", Lecturer = new Lecturer { Name = "Bulelani Mdolo" } },
                new Claim { ClaimID = 102, HoursWorked = 8, HourlyRate = 300, Status = "Rejected", Lecturer = new Lecturer { Name = "Ayanda Mbana" } }
            };
            return View();
        }
    }
}
