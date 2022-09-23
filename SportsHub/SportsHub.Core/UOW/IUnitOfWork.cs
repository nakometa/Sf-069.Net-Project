using SportsHub.Domain.Repository;

namespace SportsHub.Domain.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        Task SaveChangesAsync();
    }
}
