using GC02Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web1670.Models;

namespace Web1670.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> books = _dbContext.books.Include(p => p.Publisher).ToList();
            return View(books);
        }
        public const string CARTKEY = "cart";
        List<Cart> GetCartItems()
        {
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
        // Lưu Cart (Danh sách CartItem) vào session
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
                return NotFound("No pound book");

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.bookID == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.cartQuantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new Cart() { cartQuantity = 1, book = book });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }
        [Route("/removecart/{id:int}", Name = "removecart")]
        public IActionResult RemoveCart(int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.book.bookID == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
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
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.cartQuantity = quanty;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }


        public IActionResult DetailBook(int id)
        {
            IEnumerable<Book> books = _dbContext.books.Include(p => p.Publisher).ToList();
            var detailBook = books.Where(b => b.bookID == id).FirstOrDefault();
            return View(detailBook);
        }
        public IActionResult Create()
        {
            ViewData["pubID"] = new SelectList(_dbContext.publishers.ToList(), "pubID", "pubName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.books.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        public IActionResult Edit(int id)
        {
            ViewData["pubID"] = new SelectList(_dbContext.publishers, "pubID", "pubName");
            Book obj = _dbContext.books.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, Book obj)
        {
            if (ModelState.IsValid)
            {
                obj.bookID = id;
                _dbContext.books.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            Book obj = _dbContext.books.Find(id);
            _dbContext.books.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}