using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class ProductRepository:IProductRepository
    {
        AppDbConext conext;
        public ProductRepository(AppDbConext appDb)
        {
            conext = appDb;
        }
        public List<Product> GetAll()
        {
            List<Product> products = conext.products.ToList();
            return products;
        }

        public List<Product> Search(string query)
        {
            var result = conext.products.Where(p => p.Name.Contains(query) || p.Description.Contains(query)).ToList();
            return result;
        }

        public Product GetById(int id)
        {
            return conext.products.FirstOrDefault(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await conext.products.AddAsync(product);
            await conext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            conext.products.Update(product);
            await conext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                conext.products.Remove(product);
                await conext.SaveChangesAsync();
            }
        }
    }
}
