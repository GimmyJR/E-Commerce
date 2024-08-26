using E_Commerce.Models;
using E_Commerce.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        IOrderRepository orderRepository;
        
        public OrderController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOrderRepository orderRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.orderRepository = orderRepository;
        }
  
        public async Task<ActionResult> GetAll()
        {
            var userId = userManager.GetUserId(User);
            var orders = orderRepository.GetAll(userId);

            return View(orders);

        }
    }
}
