using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Web1670.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
