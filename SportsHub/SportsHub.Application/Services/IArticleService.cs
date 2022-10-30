using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IArticleService
    {
        Task<Article?> GetByTitleAsync(string title);
        Task<bool> CreateArticleAsync(CreateArticleDTO adminInput);
    }
}
