using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByTitleAsync(string title);

        Task<List<Article>> GetBySubstringAsync(string substring);
    }
}
