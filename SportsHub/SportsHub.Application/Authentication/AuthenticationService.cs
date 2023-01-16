using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Enumerations;
using SportsHub.Domain.PasswordHasher;

namespace SportsHub.AppService.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly IPasswordHasher passwordHasher;

        public AuthenticationService(IUserService service, IPasswordHasher hasher)
        {
            userService = service;
            passwordHasher = hasher;
        }

        public async Task<User?> Authenticate(UserLoginDTO userLogin)
        {
            var currentUser = await userService.GetByEmailOrUsernameAsync(userLogin.UsernameOrEmail);
            
            if (currentUser != null)
            {
                var checkPassword = passwordHasher.Check(currentUser.Password, userLogin.Password);

                if (checkPassword.Verified && !checkPassword.NeedsUpgrade)
                {
                    return currentUser;
                }
            }

            return null;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDTO userInput)
        {
            var usernameExists = await userService.CheckUsernameAsync(userInput.Username);

            if (usernameExists) 
            {
                //Returns false if user with the same username exists.
                return false; 
            }

            var emailExists = await userService.CheckUsernameAsync(userInput.Email);

            if (emailExists) 
            {
                //Returns false if user with the same email exists.
                return false; 
            }

            var passwordHash = passwordHasher.Hash(userInput.Password);

            var user = new User
            {
                Username = userInput.Username,
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                DisplayName = $"{userInput.FirstName} {userInput.LastName}",
                Email = userInput.Email,
                Password = passwordHash,
                ProfilePicture = null,
                RoleId = (int) RoleEnums.User
            };

            await userService.SaveUserAsync(user);

            return true;
        }
    }
}
