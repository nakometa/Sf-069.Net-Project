using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;
using System.ComponentModel;

namespace SportsHub.AppService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await unitOfWork.UserRepository.GetByEmailAsync(email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await unitOfWork.UserRepository.GetByUsernameAsync(username);
        }

        public async Task SaveUserAsync(User user)
        {
            await unitOfWork.UserRepository.SaveUserAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
