using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
        Task PostCommentAsync(Comment comment);
    }
}
