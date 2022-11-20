using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IQueryable<Comment> GetByArticle(int id, CategoryParameters categoryParameters);
    }
}
