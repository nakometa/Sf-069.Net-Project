using SportsHub.DAL.Data;
using SportsHub.DAL.Repository;
using SportsHub.Domain.Repository;
using SportsHub.Domain.UOW;

namespace SportsHub.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            UserRepository = new UserRepository(this.context);
        }

        public IUserRepository UserRepository { get; private set; }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
