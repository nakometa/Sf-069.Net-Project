using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User? GetByUsername(string username);
    }
}
