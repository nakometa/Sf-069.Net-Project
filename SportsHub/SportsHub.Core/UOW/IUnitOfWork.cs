using SportsHub.Domain.Repository;

namespace SportsHub.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IArticleRepository ArticleRepository { get; }
        ICommentRepository CommentRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICommentLikeRepository CommentLikeRepository { get; }
        Task SaveChangesAsync();
    }
}
