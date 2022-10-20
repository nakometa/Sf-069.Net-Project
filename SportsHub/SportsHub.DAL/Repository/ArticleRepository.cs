using Microsoft.EntityFrameworkCore;
using SportsHub.DAL.Data;
using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.DAL.Repository
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await DbSet
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Article>> GetByAuthorAsync(string author)
        {
            return await DbSet
                .Where(x => x.Authors.Any(a => a.DisplayName == author))
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetByStateAsync(string state)
        {
            return await DbSet
                .Include(x => x.State)
                .Where(x => x.State.Name == state)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetByCategoryAsync(string category)
        {
            return await DbSet
                .Include(x => x.Category)
                .Where(x => x.Category.Name == category)
                .ToListAsync();
        }
    }
}
