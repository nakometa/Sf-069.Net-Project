using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.AppService.Authentication;
using SportsHub.AppService.Authentication.Models.DTOs;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IJsonTokenService _tokenService;
        private IAuthenticationService _authenticationService;

        public LoginController(IJsonTokenService jsonTokenGenerator,
            IAuthenticationService authenticationService)
        {
            _tokenService = jsonTokenGenerator;
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            var user = _authenticationService.Authenticate(userLogin);

            if (user != null)
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(token);
            }

            return NotFound("Invalid username or password.");
        }
    }
}
