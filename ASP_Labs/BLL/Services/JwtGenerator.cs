using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApp.BLL.Interfaces;
using WebApp.Web.Startup.Settings;

namespace WebApp.BLL.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly AppSettings _appSettings;

        public JwtGenerator(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public string CreateToken(Guid userId, string userName, string userRole)
        {
            var claims = GetClaims(userId, userName, userRole);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.JwtSettings.TokenKey));

            var jwtToken = new JwtSecurityToken(
                issuer: _appSettings.JwtSettings.Issuer,
                audience: _appSettings.JwtSettings.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(double.Parse(_appSettings.JwtSettings.Lifetime))),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return encodedJwt;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private static ClaimsIdentity GetClaims(Guid userId, string userName, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, userRole),
                new Claim(ClaimTypes.Name, userName),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token"
            );

            return claimsIdentity;
        }
    }
}
