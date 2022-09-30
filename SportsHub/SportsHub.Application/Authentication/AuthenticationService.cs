using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;

namespace SportsHub.AppService.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;

        public AuthenticationService(IUserService service)
        {
            userService = service;
        }

        public async Task<User?> Authenticate(UserLoginDTO userLogin)
        {
            var currentUser = await userService.GetByUsernameAsync(userLogin.UsernameOrEmail)
                ?? await userService.GetByEmailAsync(userLogin.UsernameOrEmail);

            if (currentUser?.Password == userLogin.Password)
            {
                return currentUser;
            }

            return null;
        }
    }
}
