using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreBoilerplate.Application.Common.Config;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly IDateTime _dateTime;
        private readonly JwtConfig _jwtConfig;

        public JwtService(IDateTime dateTime,
            IOptions<JwtConfig> jwtConfig)
        {
            _dateTime = dateTime;
            _jwtConfig = jwtConfig.Value;
        }


        public (string, int) GenerateAccessToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = _dateTime.Now.AddSeconds(_jwtConfig.AccessTokenExpiryInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), _jwtConfig.AccessTokenExpiryInSeconds);
        }

        public (string, int) GenerateRefreshToken(int? expiriesIn = null)
        {
            string token;
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            if (!expiriesIn.HasValue)
                expiriesIn = _jwtConfig.RefreshTokenExpiryInDays;

            return (token, expiriesIn.Value);
        }
    }
}
