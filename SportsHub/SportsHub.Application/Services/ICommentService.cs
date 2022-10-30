using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
    }
}
