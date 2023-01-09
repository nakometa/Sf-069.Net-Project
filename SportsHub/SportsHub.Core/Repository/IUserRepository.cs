using SportsHub.Domain.Models;

namespace SportsHub.Domain.Repository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetByIdAsync(int id);
        public Task<User?> GetByUsernameAsync(string username);
        public Task<User?> GetByEmailAsync(string email);
        public Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail);
        public Task SaveUserAsync(User user);
        public Task DeleteArticle(Article article);
    }
}
