using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.User.Authenticate;
using NetCoreBoilerplate.Domain.Entities;
using RefreshTokenEntity = NetCoreBoilerplate.Domain.Entities.RefreshToken;

namespace NetCoreBoilerplate.Application.User.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, TokenResposeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly IDateTime _dateTime;

        public RefreshTokenCommandHandler(IUnitOfWork uow,
            IJwtService jwtService,
            IDateTime dateTime)
        {
            _uow = uow;
            _jwtService = jwtService;
            _dateTime = dateTime;
        }

        public async Task<TokenResposeDto> Handle(RefreshTokenCommand request, CancellationToken ct = default(CancellationToken))
        {
            var repo = _uow.GetCustomRepository<IAccountRepository>();

            Account user = await repo.GetByRefreshToken(request.RefreshToken, ct);
            if (user == null)
                throw new UnauthorizedException(nameof(Account), nameof(Account.RefreshTokens));

            var currentRefreshToken = user.RefreshTokens.Single(x => x.Token == request.RefreshToken);
            if (currentRefreshToken.IsExpired)
                throw new BadRequestException(nameof(RefreshTokenCommand.RefreshToken));

            (string accessToken, int accessExpiriesIn) = _jwtService.GenerateAccessToken(user);
            (string refreshToken, int refreshExpiriesIn) = _jwtService.GenerateRefreshToken();

            user.RefreshTokens.Remove(currentRefreshToken);
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
