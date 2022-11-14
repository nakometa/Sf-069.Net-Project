using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryById(int categoryId);
    }
}
