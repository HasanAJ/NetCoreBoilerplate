using NetCoreBoilerplate.Application.Common.Interfaces.Services;

namespace NetCoreBoilerplate.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string HashPassword(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }

        public bool Verify(string text, string hashedInput)
        {
            return BCrypt.Net.BCrypt.Verify(text, hashedInput);
        }
    }
}
