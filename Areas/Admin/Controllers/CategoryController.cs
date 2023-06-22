using Ecommerce.Data;
using Ecommerce.Data.Repository;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace E_commerce_new.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ICatchClauseOperation _db;
        private readonly IUnitOfWork _unitOfwork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        { 
            //List<Category> categories1 = _db.Categories.ToList();
            //IEnumerable<Category> categories = _db.FindAll();
            IEnumerable<Category> categories = _unitOfwork.Category.FindAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.createdAt = DateTime.Now.ToString();
                category.updatedAt = DateTime.Now.ToString();
                //_db.Categories.Add(category);
                _unitOfwork.Category.Create(category);
                //_db.SaveChanges();
                _unitOfwork.Save();
                TempData["success"] = "Category Added Successfully";

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            //Category category = _db.Categories.FirstOrDefault(X => X.ID == id);
            Category category = _unitOfwork.Category.FirstOrDefault(X => X.ID == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.updatedAt = DateTime.Now.ToString();
                _unitOfwork.Category.Update(category);
                //_db.SaveChanges();
                _unitOfwork.Save();
                TempData["success"] = "Category Updated Successfully";

                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }
        public IActionResult DeleteCategory(int id)
        {
            //Category category = _db.Categories.Find(id);
            Category category = _unitOfwork.Category.FirstOrDefault(X => X.ID == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //Category category = _db.Categories.Find(id);
            Category category = _unitOfwork.Category.FirstOrDefault(X => X.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfwork.Category.Delete(category);
            _unitOfwork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }
     
    }
}
