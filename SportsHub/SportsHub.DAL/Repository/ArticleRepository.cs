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

        public async Task<Article?> GetById(int id)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await FindAllAsync();
        }

        public async Task<Article?> GetByTitleAsync(string title)
        {
            return await DbSet
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();
        }
        
        public async Task<Article?> GetByIdAsync(int id)
        {
            return await FindByIdAsync(id);
        }

        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public void UpdateArticle(Article article)
        {
            _context.Update(article);
        }
        
        public async Task<List<Article>> GetBySubstringAsync(string substring)
        {
            return await DbSet
                .Where(x => x.Title.Contains(substring) ||
                            x.Authors.Any(a => a.Username.Contains(substring)))
                .ToListAsync();
        }

        public void DeleteArticle(Article article)
        {
            _context.Articles.Remove(article);
        }
    }
}