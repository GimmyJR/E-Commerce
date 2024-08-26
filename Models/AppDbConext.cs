using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace E_Commerce.Models
{
    public class AppDbConext:IdentityDbContext<IdentityUser>
    {
        public AppDbConext() : base()
        {
            
        }
        public AppDbConext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Cart>carts  { get; set; }
        public DbSet<CartItem>cartItems  { get; set; }
        public DbSet<Category>categories  { get; set; }
        public DbSet<Product>products  { get; set; }
        public DbSet<Order>orders  { get; set; }
        public DbSet<OrderItem>orderItems  { get; set; }
        


    }







}
