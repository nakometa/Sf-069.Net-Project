using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using SportsHub.Domain.Models.Constants;
using System.Security.Claims;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet("GetUserByUsername")]
        public IActionResult GetUserByUsername(string username)
        {
            var user = service.GetByUsername(username);

            if (user != null)
            {
                return Ok($"User: {user.Username}");
            }
            else
            {
                return BadRequest($"No such user");
            }
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Public property");
        }

        [HttpGet("Admins")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi, {currentUser.Username}, you are an Admin");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User()
                {
                    Username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    LastName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
                    Role = UserConstants.Users.FirstOrDefault(u => u.Username.ToLower() == ClaimTypes.NameIdentifier)?.Role
                };
            }

            return null;
        }
    }
}