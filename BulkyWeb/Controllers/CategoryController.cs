using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll().ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Create Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
            return NotFound();
            }
            var cate = _categoryRepo.Get(u => u.Id == id);
            if (cate == null) 
            {
                return NotFound();
            }
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                _categoryRepo.Update(category);
                _categoryRepo.Save();
                TempData["success"] = "Category Edit Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cate = _categoryRepo.Get(u => u.Id == id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _categoryRepo.Get(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "Category Delete Successfully";
            return RedirectToAction("Index");
        }


    }
}
