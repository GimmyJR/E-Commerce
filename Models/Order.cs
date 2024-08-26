using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }  // e.g., Pending, Shipped, Delivered
        public IdentityUser User { get; set; }  
        public ICollection<OrderItem> OrderItems { get; set; }
    }







}
