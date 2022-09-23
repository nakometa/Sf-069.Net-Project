using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByUsernameAsync(string username);
    }
}
