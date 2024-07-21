using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.Product.GetAll().ToList();
            return View(product);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            ViewData["CategoryList"] = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product Product)
        {
            //Product.CategoryId = 4;
            //Product.Category = new Category();
            //var data = _unitOfWork.Category.Get(item => item.Id.Equals(Product.CategoryId));
            //Product.Category.Id = data.Id;
            //Product.Category.Name = data.Name;
            //Product.Category.DisplayOrder = data.DisplayOrder;
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Create Successfully";
                return RedirectToAction("Index");
            }
            return View(Product);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? cate = _unitOfWork.Product.Get(item => item.Id.Equals(id));
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }

        [HttpPost]
        public IActionResult Edit(Product Product)
        {
            //    Product.CategoryId = 4;
            //    Product.Category = new Category();
            //    var data = _unitOfWork.Category.Get(item => item.Id.Equals(Product.CategoryId));
            //    Product.Category.Name = data.Name;
            //    Product.Category.DisplayOrder = data.DisplayOrder;
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Edit Successfully";
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
            var cate = _unitOfWork.Product.Get(u => u.Id == id);
            if (cate == null)
            {
                return NotFound();
            }
            return View(cate);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? Product = _unitOfWork.Product.Get(u => u.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product Delete Successfully";
            return RedirectToAction("Index");
        }


    }
}
