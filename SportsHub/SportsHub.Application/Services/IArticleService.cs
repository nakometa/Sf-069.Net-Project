using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IArticleService
    {
        Task<Article?> GetByTitleAsync(string title);
    }
}
