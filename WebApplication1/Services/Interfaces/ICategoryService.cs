using WebApplication1.Models.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> InsertCategories(IEnumerable<Category> categories);
    }
}
