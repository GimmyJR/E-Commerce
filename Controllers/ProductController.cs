using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICategoryRepsitory categoryRepsitory;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment,ICategoryRepsitory categoryRepsitory)
        {
            ProductRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.categoryRepsitory = categoryRepsitory;
        }
        
        public IActionResult GetAll()
        {
            List<Product> products = ProductRepository.GetAll();
            return View(products);
        }
        public JsonResult Search(string query)
        {
            return Json(ProductRepository.Search(query));
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["Cate"] = categoryRepsitory.GetAll();
            return View();
        }

        // POST: Product/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                await ProductRepository.AddAsync(model);
                return RedirectToAction("GetAll");
            }

            ViewData["Cate"] = categoryRepsitory.GetAll();
            return View(model);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(int id)
        {
            ProductRepository.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            return View();
        }
        public IActionResult Details(int productId)
        {
            var product = ProductRepository.GetById(productId);
            return View(product);
        }
    }
}

