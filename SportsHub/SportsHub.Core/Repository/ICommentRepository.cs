using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IQueryable<Comment> GetByArticle(int id, CategoryParameters categoryParameters);
        IQueryable<Comment> OrderByDate(int id, CategoryParameters categoryParameters);
        IQueryable<Comment> OrderByDateDescending(int id, CategoryParameters categoryParameters);
        IQueryable<Comment> SortByLikes(int id, CategoryParameters categoryParameters);
    }
}
