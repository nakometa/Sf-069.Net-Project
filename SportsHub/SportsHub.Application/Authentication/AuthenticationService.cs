using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService service;

        public AuthenticationService(IUserService service)
        {
            this.service = service;
        }

        public async Task<User?> Authenticate(UserLoginDTO userLogin)
        {
            var currentUser = await service.GetByUsernameAsync(userLogin.UsernameOrEmail) ?? await service.GetByEmailAsync(userLogin.UsernameOrEmail);

            if (currentUser?.Password == userLogin.Password)
            {
                return currentUser;
            }

            return null;
        }
    }
}
