using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;

namespace E_Commerce.Repository
{
    public interface IShoppingService
    {
        public  Task<Cart> GetCartAsync();

        public Task AddToCartAsync(int productid, int quantity);

        public Task RemoveFromCartAsync(int productId);
        public Cart GetByUserId(string userId);


    }
}
