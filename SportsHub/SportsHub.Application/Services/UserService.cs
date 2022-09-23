using SportsHub.Domain.Models;
using SportsHub.Domain.UOW;

namespace SportsHub.AppService.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await unitOfWork.UserRepository.GetByUsernameAsync(username);
        }
    }
}
