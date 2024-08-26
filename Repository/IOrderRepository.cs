using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAll(string userId);
    }
}
