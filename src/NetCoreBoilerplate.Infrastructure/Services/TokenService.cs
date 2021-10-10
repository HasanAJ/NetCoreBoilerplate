using System;
using System.Security.Cryptography;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class TokenService : ITokenService
    {

        public TokenService()
        {
        }

        public string GenerateToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
