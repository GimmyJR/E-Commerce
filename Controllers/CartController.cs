using E_Commerce.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        IShoppingService ShoppingService;
        public CartController(IShoppingService service)
        {
            ShoppingService = service;
        }
        public async Task<IActionResult> Index()
        {
            var cart =await ShoppingService.GetCartAsync();
            return View(cart);
        }
       
        public async Task<IActionResult> AddToCart(int productId ,int quantity = 1)
        {
           
            await ShoppingService.AddToCartAsync(productId,quantity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await ShoppingService.RemoveFromCartAsync(productId);
            return RedirectToAction("Index");
        }

    }
}
