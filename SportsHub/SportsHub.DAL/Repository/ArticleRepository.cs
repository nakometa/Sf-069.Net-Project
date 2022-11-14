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

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await DbSet
                .ToListAsync();
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await DbSet
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await DbSet
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
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
