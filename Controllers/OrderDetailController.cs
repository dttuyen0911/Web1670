using GC02Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web1670.Models;

namespace Web1670.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderDetailController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
            var orID = id;
            var orderdetail = _dbContext.orderdetails.Where(or => or.orderID == orID).ToList();
            return View(orderdetail);
        }
        public IActionResult Detail(int id)
        {
            var orID = id;
            var orderdetail = _dbContext.orderdetails.Where(or => or.orderID == orID).ToList();
            return View(orderdetail);
        }
    }
}
