using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
        Task AddCommentAsync(Comment comment);
        bool LikeCommentAsync(int commentId);
        bool DislikeCommentAsync(int commentId);
    }
}
