using Microsoft.AspNetCore.Mvc;

namespace CMCS.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
