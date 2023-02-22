using Microsoft.AspNetCore.Mvc;

namespace Web1670.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
