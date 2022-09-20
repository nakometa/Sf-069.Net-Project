using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;

namespace SportsHub.AppService.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public User Authenticate(UserLoginDTO userLogin)
        {
            var currentUser = UserConstants
                                .Users
                                .FirstOrDefault(u => u.Username.ToLower() == userLogin.Username.ToLower() && u.Password == userLogin.Password);


            return currentUser != null ? currentUser : null;
        }
    }
}
