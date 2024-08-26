using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.Repository
{
    public class ShoppingService:IShoppingService
    {
        private readonly AppDbConext conext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingService(AppDbConext appDb,IHttpContextAccessor httpContextAccessor)
        {
            this.conext = appDb;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart> GetCartAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await conext.carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                conext.carts.Add(cart);
                await conext.SaveChangesAsync();
            }
            return cart;
        }
        public async Task AddToCartAsync(int productid,int quantity)
        {
            var cart = await GetCartAsync();
            var cartitem = cart.CartItems.SingleOrDefault(cart => cart.ProductId == productid);
            if (cartitem == null)
            {
                cartitem =new CartItem 
                {
                    CartId = cart.Id,
                    ProductId = productid,
                    Quantity = quantity
                };
                conext.cartItems.Add(cartitem);
            }
            else
            {
                cartitem.Quantity = quantity;
            }
            await conext.SaveChangesAsync();
        }
        public async Task RemoveFromCartAsync(int productId)
        {
            var cart =await GetCartAsync();
            var cartItem = cart.CartItems.FirstOrDefault(cart => cart.ProductId == productId);
            if (cartItem!=null)
            {
                conext.cartItems.Remove(cartItem);
                await conext.SaveChangesAsync();
            }
        }

        public Cart GetByUserId(string userId)
        {
            return conext.carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);
        }


    }
}
