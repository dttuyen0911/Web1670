using Microsoft.AspNetCore.Mvc;

namespace Web1670.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
