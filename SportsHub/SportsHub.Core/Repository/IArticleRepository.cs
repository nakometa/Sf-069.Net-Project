using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IArticleRepository
    {
        Task<Article?> GetByTitleAsync(string title);
        Task<IEnumerable<Article>> GetByAuthorAsync(string author);
        Task<IEnumerable<Article>> GetByCategoryAsync(string category);
    }
}
