using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentLikeRepository : IGenericRepository<CommentLike>
    {
        Task<CommentLike?> GetLikeAsync(int commentId, int userId);
    }
}
