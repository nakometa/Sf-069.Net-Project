using SportsHub.Domain.Models;

namespace SportsHub.AppService.Authentication
{
    public interface IJsonTokenService
    {
        public string GenerateToken(User user);
    }
}
