using Ecommerce.Data;
using Ecommerce.Data.Repository;
using Ecommerce.Data.Repository.IRepository;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace E_commerce_new.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        //private readonly ICatchClauseOperation _db;
        private readonly IUnitOfWork _unitOfwork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        { 
            //List<Product> products1 = _db.Products.ToList();
            //IEnumerable<Product> products = _db.FindAll();
            IEnumerable<Product> products = _unitOfwork.Product.FindAll();
            return View(products);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Product product)
        {
            if (ModelState.IsValid)
            {
                product.createdAt = DateTime.Now.ToString();
                product.updatedAt = DateTime.Now.ToString();
                //_db.Categories.Add(category);
                _unitOfwork.Product.Create(product);
                //_db.SaveChanges();
                _unitOfwork.Save();
                TempData["success"] = "Product Added Successfully";

                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            //Product product = _db.Products.FirstOrDefault(X => X.ID == id);
            Product product = _unitOfwork.Product.FirstOrDefault(X => X.ID == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Rating = 1;
                product.updatedAt = DateTime.Now.ToString();
                _unitOfwork.Product.Update(product);
                //_db.SaveChanges();
                _unitOfwork.Save();
                TempData["success"] = "Product Updated Successfully";

                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }
        public IActionResult DeleteProduct(int id)
        {
            //Product product = _db.Products.Find(id);
            Product product = _unitOfwork.Product.FirstOrDefault(X => X.ID == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //product product = _db.Products.Find(id);
            Product product = _unitOfwork.Product.FirstOrDefault(X => X.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfwork.Product.Delete(product);
            _unitOfwork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");

        }
       
    }
}
