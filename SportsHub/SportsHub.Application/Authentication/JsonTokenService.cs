using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportsHub.AppService.Authentication.Models.Options;
using SportsHub.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SportsHub.AppService.Authentication
{
    public class JsonTokenService : IJsonTokenService
    {

        private JsonTokenOptions _tokenOptions;

        public JsonTokenService(IOptions<JsonTokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
