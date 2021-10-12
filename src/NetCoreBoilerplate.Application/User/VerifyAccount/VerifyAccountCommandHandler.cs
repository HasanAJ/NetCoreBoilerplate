using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Exceptions;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Application.User.VerifyAccount
{
    public class VerifyAccountCommandHandler : ICommandHandler<VerifyAccountCommand, VoidResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTime _dateTime;

        public VerifyAccountCommandHandler(IUnitOfWork uow,
            IDateTime dateTime)
        {
            _uow = uow;
            _dateTime = dateTime;
        }

        public async Task<VoidResult> Handle(VerifyAccountCommand request, CancellationToken ct = default(CancellationToken))
        {
            var repo = _uow.GetRepository<Account>();

            var user = await repo.Find(i => i.VerificationToken == request.VerificationToken, ct: ct);

            if (user == null)
                throw new BadRequestException(nameof(VerifyAccountCommand.VerificationToken));

            user.VerifiedOn = _dateTime.Now;
            user.VerificationToken = null;

            repo.Update(user);
            await _uow.SaveChanges(ct);

            return new VoidResult();
        }
    }
}
