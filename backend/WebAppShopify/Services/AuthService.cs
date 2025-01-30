using WebAppShopify.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAppShopify.Configuration;
using WebAppShopify.Data;
using WebAppShopify.Models;
using WebAppShopify.DTOs;

namespace WebAppShopify.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(IOptions<JwtSettings> jwtSettings, ApplicationDbContext dbContext)
        {
            _jwtSettings = jwtSettings.Value;
            _dbContext = dbContext;
        }

        public async Task<AuthResponseDTO> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash ))
            {
                return null; // Return null if invalid credentials
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthResponseDTO
            {
                AccessToken = tokenString,
                IssuedAt = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()
            };
        }
    }
}
