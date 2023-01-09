using SportsHub.Domain.Models;
using System.Security.Claims;

namespace SportsHub.AppService.Services
{
    public interface IUserService
    {
        public Task<User?> GetByUsernameAsync(string username);
        public Task<bool> CheckUsernameAsync(string username);
        public Task<User?> GetByEmailAsync(string email);
        public Task<bool> CheckEmailAsync(string email);
        public Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail);
        public Task<User?> GetUserByClaimsAsync(ClaimsIdentity identity);
        public Task SaveUserAsync(User user);
    }
}
