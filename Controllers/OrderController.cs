using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Net.WebSockets;
using System.Security.Claims;
using Web1670.Models;

namespace Web1670.Controllers
{
    public class OrderController : Controller
    {
        public const string CARTKEY = "cart";
        private readonly ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Admin,Owner,User")]
        public IActionResult Index()
        {
            ViewBag.CurrentDate = DateTime.Now;
            return View();
        }
        [Authorize(Roles = "Admin,Owner,User")]
        public IActionResult DisplayOrder(Order obj)

        {
            var userID = GetID();
            List<Order> orders = _dbContext.orders.Where(o => o.cus_id == userID).ToList();
            return View(orders);
        }
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Detail(Order obj)
        {
            List<Order> order = _dbContext.orders.ToList();
            return View(order);
        }
        [Authorize(Roles = "Admin,Owner,User")]
        [HttpPost]
        public async Task<IActionResult> Create(Order obj)
        {
            if (obj.orderFullname == null || obj.orderAddress == null || obj.orderPhone == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var userID = GetID();
                obj.cus_id = userID;

                var cart = GetCartItems();
                Order or = new Order();
                or = obj;
                or.cus_id = userID;
                or.owner_id = "jkasdf";
                or.OrderTotal = 0;
                or.orderDate = DateTime.Now;
                or.orderAddress = obj.orderAddress;
                or.orderFullname = obj.orderFullname;
                or.orderPhone = obj.orderPhone;
                or.orderdetails = new List<OrderDetail>();
                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        OrderDetail od = new OrderDetail();
                        od.bookID = item.book.bookID;
                        od.quantity = item.cartQuantity;
                        od.price += item.cartQuantity * item.book.bookPrice;
                        or.OrderTotal += od.price;
                        od.orderID = or.orderID;
                        od.amount = od.price * od.quantity;
                        od.OrderDetailDate = or.orderDate;
                        int qu = GetQuantityBook();
                        int 

                        or.orderdetails.Add(od);
                        //string data = JsonSerializer.Serialize<Order>(or);
                    }
                }
                _dbContext.orders.Add(or);
                _dbContext.SaveChanges();
                ClearCart();
                return RedirectToAction("DisplayOrder");
            }
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        void SaveCartSession(List<Cart> ls)
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var KEY = CARTKEY + userID;
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(KEY, jsoncart);
        }
        List<Cart> GetCartItems()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var KEY = CARTKEY;
            var session = HttpContext.Session;
            string jsoncart = session.GetString(KEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }
        public string GetID()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var KEY = userID.ToString();
            return KEY;
        }
        public int GetQuantityBook(Book obj)
        {
            var q = obj.bookQuantity;
            return (int)q;
        }
    }
}
