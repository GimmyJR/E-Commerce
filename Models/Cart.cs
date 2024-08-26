using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }  
        public IdentityUser User { get; set; }   
    }
}
