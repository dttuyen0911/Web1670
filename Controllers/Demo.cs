using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GC02Identity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Demo : Controller
    {
        [AllowAnonymous]
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
