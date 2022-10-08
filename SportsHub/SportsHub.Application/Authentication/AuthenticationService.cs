using SportsHub.AppService.Authentication.Models.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
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
            var currentUser = await userService.GetByUsernameAsync(userLogin.UsernameOrEmail)
                ?? await userService.GetByEmailAsync(userLogin.UsernameOrEmail);
            
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

        public async Task<bool> RegisterUser(UserRegisterDTO userInput)
        {
            //Returns false if user with the same username exists.
            var usernameExists = await userService.GetByUsernameAsync(userInput.Username) != null;
            if (usernameExists) { return false; }
            //Returns false if user with the same email exists.
            var emailExists = await userService.GetByEmailAsync(userInput.Email) != null;
            if (emailExists) { return false; }

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
                RoleId = 1
            };

            await userService.SaveUserAsync(user);

            return true;
        }
    }
}
