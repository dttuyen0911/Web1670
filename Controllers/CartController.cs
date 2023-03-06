using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Claims;
using Web1670.Models;

namespace Web1670.Controllers
{
    [Authorize(Roles = "Admin,Owner,User")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Cart> cart = _dbContext.carts.ToList();
            return View(cart);
        }
        public const string CARTKEY = "cart";
        List<Cart> GetCartItems()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var KEY = CARTKEY + userID.ToString();
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(Cart obj)
        //{
        //    var userID = GetID();
        //    obj.cus_id = userID;

        //    var cart = GetCartItems();
        //    Cart carts = new Cart();
        //    carts = obj;
        //    carts.cus_id = userID;
        //    carts.cartID = obj.cartID;
        //    carts.cartQuantity = obj.cartQuantity;
        //    carts.book.bookID = obj.book.bookID;
        //    _dbContext.carts.Add(carts);
        //    _dbContext.SaveChanges();
        //    return View(carts);
        //}
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }
        void SaveCartSession(List<Cart> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

        public IActionResult AddToCart(int id)
        {
            var book = _dbContext.books.Where(p => p.bookID == id).FirstOrDefault();
            if (book == null)
                return NotFound("Cart empty");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.bookID == id);
            if (cartitem != null)
            {
                cartitem.cartQuantity++;
            }
            else
            {
                cart.Add(new Cart() { cartQuantity = 1, book = book });
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        [Route("/removecart/{id:int}", Name = "removecart")]
        [Authorize(Roles = "Admin,Owner,User")]
        public IActionResult RemoveCart(int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.bookID == id);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        [HttpPost]
        [Route("/updatecart", Name = "updatecart")]
        public IActionResult UpdateCart(int id, int quanty)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.bookID == id);
            if(cartitem != null)
            {
                if(quanty < 0)
                {
                    return RedirectToAction(nameof(Cart));
                }
                else
                {
                    cartitem.cartQuantity = quanty;
                }
            }
            SaveCartSession(cart);
            return Ok();
        }
    }
}
