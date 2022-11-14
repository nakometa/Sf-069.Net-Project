using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            return await DbSet
                .Where(x => x.Id == categoryId)
                .FirstOrDefaultAsync();
        }
    }
}
