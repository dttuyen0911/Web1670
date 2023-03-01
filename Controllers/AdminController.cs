using Microsoft.AspNetCore.Mvc;

namespace Web1670.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
