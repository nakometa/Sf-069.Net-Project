using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IUserService
    {
        public Task<User?> GetByUsernameAsync(string username);
    }
}
