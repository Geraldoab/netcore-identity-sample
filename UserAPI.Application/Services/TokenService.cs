using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Domain.Interfaces.Services;

namespace UserAPI.Application.Services
{
    public class TokenService : ITokenService
    {
        public TokenService()
        {
        }

        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id),
                new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SYMMETRIC_SECURITY_KEY")));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                  expires: DateTime.Now.AddMinutes(10),
                  claims: claims,
                  signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
