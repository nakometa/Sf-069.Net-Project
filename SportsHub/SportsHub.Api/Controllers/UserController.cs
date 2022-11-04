using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Api.DTOs;
using SportsHub.AppService.Services;
using SportsHub.Domain.Models;
using System.Security.Claims;

namespace SportsHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsernameAsync(string username)
        {
            var user = await _service.GetByUsernameAsync(username);

            if (user == null) return BadRequest($"No such user");

            UserResponseDTO userResponseDto = _mapper.Map<UserResponseDTO>(user);
            return Ok($"User: {userResponseDto.Username}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Public property");
        }

        [HttpGet("Admins")]
        public async Task<IActionResult> AdminsEndpoint()
        {
            var currentUser = await GetCurrentUserByClaimsAsync();
            UserResponseDTO adminUserDto = _mapper.Map<UserResponseDTO>(currentUser);

            return Ok($"Hi, {adminUserDto.Username}, you are an Admin");
        }

        private async Task<User?> GetCurrentUserByClaimsAsync()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var currentUser = await _service.GetUserByClaimsAsync(identity);

            return currentUser;
        }
    }
}