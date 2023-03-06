using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
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
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Index()
        {
            IEnumerable<Book> books = _dbContext.books.ToList();
            return View(books);
        }
        public IActionResult DetailBook(int id)
        {
            IEnumerable<Book> books = _dbContext.books.ToList();
            var detailBook = books.Where(b => b.bookID == id).FirstOrDefault();
            return View(detailBook);
        }
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Create()
        {
            ViewData["pubID"] = new SelectList(_dbContext.publishers.ToList(), "pubID", "pubName");
            ViewData["cateID"] = new SelectList(_dbContext.categories.ToList(), "cateID", "cateName");
            return View();
        }
        [Authorize(Roles = "Admin,Owner")]
        [HttpPost]
        public IActionResult Create(Book obj)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadFile(obj);
                obj.urlImage = fileName;

                _dbContext.books.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["pubID"] = new SelectList(_dbContext.publishers.ToList(), "pubID", "pubName");
            ViewData["cateID"] = new SelectList(_dbContext.categories.ToList(), "cateID", "cateName");
            return View(obj);
        }
        [Authorize(Roles = "Admin,Owner")]
        public string UploadFile(Book obj)
        {
            string uniqueFileName = null;
            if (obj.Image != null)
            {
                string uploadsFoder = Path.Combine("wwwroot", "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + obj.bookID + obj.Image.FileName;
                string filePath = Path.Combine(uploadsFoder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int id)
        {
            ViewData["pubID"] = new SelectList(_dbContext.publishers, "pubID", "pubName");
            ViewData["cateID"] = new SelectList(_dbContext.categories.ToList(), "cateID", "cateName");
            Book obj = _dbContext.books.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [Authorize(Roles = "Admin,Owner")]
        [HttpPost]
        public IActionResult Edit(int id, Book obj, string img)
        {
            if (ModelState.IsValid)
            {
                if (obj.Image == null)
                {
                    obj.urlImage = img;
                    _dbContext.books.Update(obj);
                    _dbContext.SaveChanges();
                }
                else
                {
                    obj.bookID = id;
                    string uniqueFileName = UploadFile(obj);
                    obj.urlImage = uniqueFileName;
                    _dbContext.books.Update(obj);
                    _dbContext.SaveChanges();
                    img = Path.Combine("wwwroot", "uploads", img);
                    FileInfo infor = new FileInfo(img);
                    if (infor != null)
                    {
                        System.IO.File.Delete(img);
                        infor.Delete();
                    }
                }return RedirectToAction("Index");
            }
            ViewData["pubID"] = new SelectList(_dbContext.publishers, "pubID", "pubName");
            ViewData["cateID"] = new SelectList(_dbContext.categories.ToList(), "cateID", "cateName");
            return View(obj);
        }
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Delete(int id, string img)
        {
            Book obj = _dbContext.books.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (obj.urlImage == null)
                {
                    _dbContext.books.Remove(obj);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    img = Path.Combine("wwwroot", "uploads", img);
                    FileInfo infor = new FileInfo(img);
                    if (infor != null)
                    {
                        System.IO.File.Delete(img);
                        infor.Delete();
                    }
                    _dbContext.books.Remove(obj);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}