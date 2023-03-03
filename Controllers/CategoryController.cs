using GC02Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web1670.Models;

namespace Web1670.Controllers
{
    [Authorize(Roles = "Admin,Owner")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> category = _dbContext.categories.ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.categories.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            Category obj = _dbContext.categories.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, Category obj)
        {
            if (ModelState.IsValid)
            {
                obj.cateID = id;
                _dbContext.categories.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Delete(int id)
        {
            var obj = _dbContext.categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.categories.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
