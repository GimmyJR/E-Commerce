using E_Commerce.Models;
using System;

namespace E_Commerce.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> Search(string query);
        Product GetById(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        
    }
}
