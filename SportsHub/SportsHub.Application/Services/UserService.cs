using SportsHub.Domain.Models;
using SportsHub.Domain.Repository;

namespace SportsHub.AppService.Services
{
    public class UserService : IUserService
    {
        public UserService(IUnitOfWork uow)
        {

        }

        public User GetByPassword()
        {
            throw new NotImplementedException();
        }
    }
}
