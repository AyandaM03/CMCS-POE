using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
                ViewData["ErrorMessage"] = $"Error {statusCode}: Something went wrong.";
            else
                ViewData["ErrorMessage"] = "An unexpected error occurred.";
            return View();
        }

    }
}
