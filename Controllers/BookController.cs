using GC02Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
