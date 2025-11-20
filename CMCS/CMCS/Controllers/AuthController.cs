using Microsoft.AspNetCore.Mvc;
using CMCS.Data;
using CMCS.Models;

namespace CMCS.Controllers
{
    public class AuthController : Controller
    {
        private readonly CMCSContext _context;

        public AuthController(CMCSContext context)
        {
            _context = context;
        }

        // -------------------------------
        // LOGIN
        // -------------------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Lecturers
                               .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                TempData["Error"] = "Invalid login details.";
                return View();
            }

            return RedirectToAction("Index", "Lecturer");
        }

        // -------------------------------
        // REGISTER (GET)
        // -------------------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // -------------------------------
        // REGISTER (POST)
        // -------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Lecturer model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Set default role
            model.RoleID = 1;

            _context.Lecturers.Add(model);
            _context.SaveChanges();

            TempData["Message"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }
    }
}


