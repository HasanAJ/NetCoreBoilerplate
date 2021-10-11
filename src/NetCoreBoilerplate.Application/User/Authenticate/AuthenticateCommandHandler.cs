using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Entities;
using RefreshTokenEntity = NetCoreBoilerplate.Domain.Entities.RefreshToken;

namespace NetCoreBoilerplate.Application.User.Authenticate
{
    public class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, TokenResposeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly IDateTime _dateTime;
        private readonly IHashService _hashService;

        public AuthenticateCommandHandler(IUnitOfWork uow,
            IJwtService jwtService,
            IDateTime dateTime,
            IHashService hashService)
        {
            _uow = uow;
            _jwtService = jwtService;
            _dateTime = dateTime;
            _hashService = hashService;
        }

        public async Task<TokenResposeDto> Handle(AuthenticateCommand request, CancellationToken ct = default(CancellationToken))
        {
            var repo = _uow.GetCustomRepository<IAccountRepository>();

            Account user = await repo.GetByEmail(request.Email, ct);
            if (user == null)
                throw new UnauthorizedException(nameof(Account), nameof(Account.Email));

            bool isCorrectPassword = _hashService.Verify(request.Password, user.Password);
            if (!isCorrectPassword)
                throw new UnauthorizedException(nameof(Account), nameof(Account.Password));

            (string accessToken, int accessExpiriesIn) = _jwtService.GenerateAccessToken(user);
            (string refreshToken, int refreshExpiriesIn) = _jwtService.GenerateRefreshToken();

            user.RefreshTokens = user.RefreshTokens.Where(i => !i.IsExpired).ToList();
            user.RefreshTokens.Add(new RefreshTokenEntity
            {
                Token = refreshToken,
                ExpiresOn = _dateTime.Now.AddDays(refreshExpiriesIn)
            });

            repo.Update(user);
            await _uow.SaveChanges(ct);

            return new TokenResposeDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = accessExpiriesIn,
            };
        }
    }
}
