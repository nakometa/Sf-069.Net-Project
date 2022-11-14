using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByArticleAsync(int id);
        Task AddCommentAsync(Comment comment);
    }
}
