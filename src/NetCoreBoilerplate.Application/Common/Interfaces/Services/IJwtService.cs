using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Application.Common.Interfaces.Services
{
    public interface IJwtService
    {
        (string, int) GenerateAccessToken(Account account);

        (string, int) GenerateRefreshToken(int? expiriesIn = null);
    }
}
