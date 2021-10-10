using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Entities;

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
            var repo = _uow.GetRepository<Account>();

            Account user = await repo.Find(i => i.Email == request.Email);

            if (user == null)
                throw new UnauthorizedException(nameof(Account), nameof(Account.Email));

            bool isCorrectPassword = _hashService.Verify(request.Password, user.Password);
            if (!isCorrectPassword)
                throw new NotFoundException(nameof(Account), nameof(Account.Password));

            (string accessToken, int accessExpiriesIn) = _jwtService.GenerateAccessToken(user);
            (string refreshToken, int refreshExpiriesIn) = _jwtService.GenerateRefreshToken();

            user.RefreshTokens.Add(new Domain.Entities.RefreshToken()
            {
                Token = refreshToken,
                ExpiresOn = _dateTime.Now.AddDays(refreshExpiriesIn)
            });

            // TODO: remove old/used refresh tokens in the background

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
