using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Entities;
using NetCoreBoilerplate.Domain.Enums;
using NetCoreBoilerplate.Domain.Events.User;

namespace NetCoreBoilerplate.Application.User.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, VoidResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;

        public RegisterUserCommandHandler(IUnitOfWork uow,
            IJwtService jwtService,
            ITokenService tokenService,
            IHashService hashService)
        {
            _uow = uow;
            _jwtService = jwtService;
            _tokenService = tokenService;
            _hashService = hashService;
        }

        public async Task<VoidResult> Handle(RegisterUserCommand request, CancellationToken ct = default(CancellationToken))
        {
            var repo = _uow.GetRepository<Account>();

            Account user = await repo.Find(i => i.Email == request.Email);

            if (user != null)
                throw new DuplicateKeyException(nameof(Account), nameof(Account.Email));

            user = new Account()
            {
                Email = request.Email,
                Password = _hashService.HashPassword(request.Password),
                Role = Role.User,
                VerificationToken = _tokenService.GenerateToken()
            };

            await repo.Insert(user, ct);

            user.DomainEvents.Add(new UserCreatedEvent(user.Email, user.VerificationToken));

            await _uow.SaveChanges(ct);

            return new VoidResult();
        }
    }
}
