using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web1670.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(ApplicationDbContext dbContext, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> serManager)
        {
            
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager= serManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowAccount()
        {
            var account = _dbContext.Users.ToList();
            return View(account);
        }
    }
}
