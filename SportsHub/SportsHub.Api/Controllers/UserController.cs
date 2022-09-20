using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.Controllers.ControllerHelpers;
using System.Security.Claims;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Public property");
        }

        [HttpGet("Admins")]
        public IActionResult AdminsEndpoint()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var currentUser = UserControllerHelper.GetCurrentUser(identity);
            return Ok($"Hi, {currentUser.Username}, you are an Admin");
        }
    }
}