using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;
using System.Security.Claims;

namespace SportsHub.AppService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string usernameOrEmail)
        {
            return await unitOfWork.UserRepository.GetByUsernameOrEmailAsync(usernameOrEmail);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _unitOfWork.UserRepository.GetByEmailAsync(email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task<User?> GetUserByClaimsAsync(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var userClaims = identity.Claims;
            var username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // Maybe throw exception if user is null in future.
            var user = await unitOfWork.UserRepository.GetByUsernameAsync(username);

            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        public async Task SaveUserAsync(User user)
        {
            await unitOfWork.UserRepository.SaveUserAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
