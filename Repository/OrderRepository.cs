using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class OrderRepository:IOrderRepository
    {
        AppDbConext conext;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderRepository(AppDbConext appDb,UserManager<IdentityUser> userManager)
        {
            conext = appDb;
            _userManager = userManager;
        }
        public List<Order> GetAll(string userId)
        {

            var orders = conext.orders.Where(o => o.UserId == userId).ToList();
            return orders;
        }
    }
}
