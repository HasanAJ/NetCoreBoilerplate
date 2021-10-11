using System;
using System.Threading;
using System.Threading.Tasks;
using NetCoreBoilerplate.Application.Common.Interfaces.Database;
using NetCoreBoilerplate.Application.Common.Interfaces.Services;
using NetCoreBoilerplate.Application.Common.Mediator;
using NetCoreBoilerplate.Domain.Entities;
using NetCoreBoilerplate.Domain.Events.User;

namespace NetCoreBoilerplate.Application.User.ChangePassword
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, VoidResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IHashService _hashService;
        private readonly IContext _context;

        public ChangePasswordCommandHandler(IUnitOfWork uow,
            IHashService hashService,
            IContext context)
        {
            _uow = uow;
            _hashService = hashService;
            _context = context;
        }

        public async Task<VoidResult> Handle(ChangePasswordCommand request, CancellationToken ct = default(CancellationToken))
        {
            bool isOldPasswordCorrect = _hashService.Verify(request.OldPassword, _context.Account.Password);

            if (!isOldPasswordCorrect)
                throw new Exception("invalid old password");

            var user = await _uow.GetRepository<Account>().Find(_context.Account.Id, ct);
            user.Password = _hashService.HashPassword(request.NewPassword);

            user.DomainEvents.Add(new UserChangedPasswordEvent(user.Email));

            await _uow.SaveChanges();

            return new VoidResult();
        }
    }
}
