using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nasa.Common.DTO;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nasa.BLL.JWT
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUserDTO> _userManager;

        public JwtService(IConfiguration configuration, UserManager<ApplicationUserDTO> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<AuthorizationResponse> GetJwt(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration_minutes"]));

            string role = string.Join(',', (await _userManager.GetRolesAsync(user)));

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.userName),
                new Claim(JwtRegisteredClaimNames.Email, user.email),
            };

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken tokenGenerator = new(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(tokenGenerator);

            Guid userId = user.UserId is null ? user.Id : user.UserId.Value;

            return new AuthorizationResponse { UserId = userId, Role = role, Email = user.Email, Expiration = expiration, Token = token };
        }
    }
}
