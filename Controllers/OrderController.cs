using GC02Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Globalization;
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
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order obj)
        {
            
            var userID = GetID();
            obj.cus_id = userID;
            
            var cart = GetCartItems();
            Order or = new Order();
            or = obj;
            or.cus_id = userID;
            or.owner_id = "jkasdf";
            or.OrderTotal = 0;
            or.orderAddress = obj.orderAddress;
            or.orderFullname = obj.orderFullname;
            or.orderdetails = new List<OrderDetail>();
            foreach (var item in cart)
            {
               
                OrderDetail od = new OrderDetail();
                od.bookID = item.book.bookID;
                od.quantity = item.cartQuantity;
                od.price += item.cartQuantity * item.book.bookPrice;
                or.OrderTotal += od.price;
                od.orderID = or.orderID;
                od.amount = od.price * od.quantity;
                or.orderdetails.Add(od);
                //string data = JsonSerializer.Serialize<Order>(or);
            }

             _dbContext.orders.Add(or);
                _dbContext.SaveChanges();
            ClearCart();
               return RedirectToAction("Index", "Order");
            return View();
        }
        void ClearCart()
        {

            List<Cart> cart = GetCartItems();
            
            cart.Clear();
            SaveCartSession(cart);

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
            var KEY = CARTKEY + userID.ToString();
            return KEY;
        }
    }
}
