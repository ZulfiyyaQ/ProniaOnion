using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion.Application.Abstraction.Services;
using ProniaOnion.Application.DTOs.Tokens;
using ProniaOnion.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProniaOnion.Infrastructure.Implementations
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _config;
        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }


        public TokenResponseDto CreateJwt(AppUser user, int minutes)
        {
            ICollection<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname)

            };
            SymmetricSecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));

            SigningCredentials signing = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Auidence"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: signing
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenResponseDto dto = new TokenResponseDto(handler.WriteToken(token), token.ValidTo, user.UserName);
            return dto;
        }
    }
}
