using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public class CategoryRepsitory:ICategoryRepsitory
    {
        private readonly AppDbConext conext;

        public CategoryRepsitory(AppDbConext conext)
        {
            this.conext = conext;
        } 
        public async Task AddCategory(Category category)
        {
            await conext.categories.AddAsync(category);
            await conext.SaveChangesAsync();
        }

        public List<Category> GetAll()
        {
            List<Category> categories = conext.categories.ToList();
            return categories;
        }
    }
   
}
