using SportsHub.Domain.Models;

namespace SportsHub.AppService.Services
{
    public interface IUserService
    {
        User? GetByUsername(string username);
    }
}
