using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IArticleService
    {
        Task<Article?> GetByTitleAsync(string title);
        Task<IEnumerable<Article>> GetByAuthorAsync(string author);
        Task<IEnumerable<Article>> GetByStateAsync(string state);
        Task<IEnumerable<Article>> GetByCategoryAsync(string category);
    }
}
