using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article?> GetByTitleAsync(string title);
    }
}
