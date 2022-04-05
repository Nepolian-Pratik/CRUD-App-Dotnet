using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public IActionResult Index()
        {
            IEnumerable<Category> ObjCategoriesList = _db.Categories;
            return View(ObjCategoriesList);
        }
        //GET Action Method
        public IActionResult Create()
        {
            return View();
        }
        //POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("DisplayOrder", "Display Order should be different from Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET Action Method
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Categoryfromdb = _db.Categories.Find(id);
            if (Categoryfromdb == null)
            {
                return NotFound();
            }
            //var CategoryfromdbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var CategoryfromdbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            return View(Categoryfromdb);
        }
        //POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "Display Order should be different from Name");
            }
            if (ModelState.IsValid)
                {
                    _db.Categories.Update(obj);
                    _db.SaveChanges();
                   TempData["success"] = "Category Updated Successfully!";
                   return RedirectToAction("Index");
                }
            return View(obj);
        }
        //GET Action Method
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var Categoryfromdb = _db.Categories.Find(id);
            if (Categoryfromdb == null)
            {
                return NotFound();
            }
            //var CategoryfromdbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var CategoryfromdbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            return View(Categoryfromdb);
        }
        //POST Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEntry(int? id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully!";
            return RedirectToAction("Index");
        }
    }
}
