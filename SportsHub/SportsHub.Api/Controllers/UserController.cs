﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsHub.Domain.Models.Constants;
using SportsHub.Domain.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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