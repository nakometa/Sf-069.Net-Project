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

        public async Task<List<Article>> GetBySubstringAsync(string substring)
        {
            return await DbSet
                .Where(x => x.Title.Contains(substring) ||
                    x.Authors.Any(a => a.Username.Contains(substring)))
                .ToListAsync();
        }
    }
}
