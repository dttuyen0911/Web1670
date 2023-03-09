using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
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
                if(quanty > cartitem.book.bookQuantity)
                {
                    ModelState.AddModelError("Quantity", "Quantity must not exceed the available stock");
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
