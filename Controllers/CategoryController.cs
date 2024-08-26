using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepsitory categoryRepsitory;

        public CategoryController(ICategoryRepsitory categoryRepsitory)
        {
            this.categoryRepsitory = categoryRepsitory;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryRepsitory.AddCategory(category);
                
            }
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            List<Category> categories = categoryRepsitory.GetAll();
            return View(categories);
        }
    }
}
