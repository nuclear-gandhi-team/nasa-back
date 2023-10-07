using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.Auth;
using Nasa.Common.DTO.User;
using Nasa.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nasa.BLL.Services.JWT
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtService(IConfiguration configuration, IMapper mapper, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _configuration = configuration;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<AuthorizationResponse> GetJwt(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var jwt = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                _jwtOptions.NotBefore,
                _jwtOptions.Expiration,
                _jwtOptions.SigningCredentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwt);

            var result = _mapper.Map<AuthorizationResponse>(user);

            result.TokenDto = new TokenDto
            {
                Token = token,
            };

            return result;
        }
    }
}
