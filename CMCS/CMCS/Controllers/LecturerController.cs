using System.Diagnostics;
using CMCS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class LecturerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}