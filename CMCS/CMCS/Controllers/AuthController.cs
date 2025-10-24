using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // shows Views/Auth/Login.cshtml
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
           
            return RedirectToAction("Index", "Lecturer");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
    }
}
