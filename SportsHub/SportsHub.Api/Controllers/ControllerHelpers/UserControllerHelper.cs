using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.Models;
using System.Security.Claims;

namespace SportsHub.Api.Controllers.ControllerHelpers
{
    public class UserControllerHelper
    {
        public static User GetCurrentUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User()
                {
                    Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    LastName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value
                    // Should we retrieve the user from the DB in order to return his role inside the JWT Token as well?
                    //Role = UserConstants.Users.FirstOrDefault(u => u.Username.ToLower() == ClaimTypes.NameIdentifier)?.Role
                };
            }

            return null;
        }
    }
}
