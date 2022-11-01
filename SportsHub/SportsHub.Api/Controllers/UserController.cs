using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.ControllerConstants;
using SportsHub.Domain.Models;
using System.Security.Claims;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsernameAsync(string username)
        {
            var user = await _userService.GetByUsernameAsync(username);

            if (user == null) return BadRequest(UserControllerConstants.UserNotFound);

            UserResponseDTO userResponseDto = _mapper.Map<UserResponseDTO>(user);
            return Ok(string.Format(UserControllerConstants.UserFound, userResponseDto.Username));
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok(UserControllerConstants.AdminEndpoint);
        }

        [HttpGet("Admins")]
        public async Task<IActionResult> AdminsEndpoint()
        {
            var currentUser = await GetCurrentUserByClaimsAsync();
            UserResponseDTO adminUserDto = _mapper.Map<UserResponseDTO>(currentUser);

            return Ok(string.Format(UserControllerConstants.AdminEndpoint, adminUserDto.Username));
        }

        private async Task<User?> GetCurrentUserByClaimsAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var currentUser = await _userService.GetUserByClaimsAsync(identity);

            return currentUser;
        }
    }
}