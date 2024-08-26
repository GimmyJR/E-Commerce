using E_Commerce.Models;

namespace E_Commerce.Repository
{
    public interface ICategoryRepsitory
    {
        Task AddCategory(Category category);
        List<Category> GetAll();
    }
}
