namespace SportsHub.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
