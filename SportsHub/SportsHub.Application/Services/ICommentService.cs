using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
        Task<bool> AddCommentAsync(CreateCommentDTO commentInput);
        Task<bool> LikeCommentAsync(int commentId);
        Task<bool> DislikeCommentAsync(int commentId);
    }
}
