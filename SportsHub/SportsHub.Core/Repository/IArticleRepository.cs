using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IArticleRepository : IGenericRepository<Article>
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByTitleAsync(string title);
        Task<Article?> GetByIdAsync(int id);
        Task AddArticleAsync(Article article);
        void UpdateArticle(Article article);
        Task<List<Article>> GetBySubstringAsync(string substring);
        void DeleteArticle(Article article);
    }
}
