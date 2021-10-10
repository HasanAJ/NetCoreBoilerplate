using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Application.User.Authenticate;

namespace NetCoreBoilerplate.Application.User.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, TokenResposeDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly IDateTime _dateTime;
        private readonly IHashService _hashService;

        public RefreshTokenCommandHandler(IUnitOfWork uow,
            IJwtService jwtService,
            IDateTime dateTime,
            IHashService hashService)
        {
            _uow = uow;
            _jwtService = jwtService;
            _dateTime = dateTime;
            _hashService = hashService;
        }

        public async Task<TokenResposeDto> Handle(RefreshTokenCommand request, CancellationToken ct = default(CancellationToken))
        {
            // TODO: implement

            await Task.CompletedTask;

            return new TokenResposeDto();
        }
    }
}
