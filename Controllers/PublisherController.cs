using GC02Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Web1670.Models;

namespace Web1670.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public PublisherController (ApplicationDbContext dbContext)// được tạo từ đăng kí dịch vụ
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = _dbContext.publishers.ToList();
            return View(publishers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.publishers.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int id)
        {
            Publisher obj = _dbContext.publishers.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, Publisher obj)
        {
            if (ModelState.IsValid)
            {
                obj.pubID = id;
                _dbContext.publishers.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            Publisher obj = _dbContext.publishers.Find(id);
            _dbContext.publishers.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
