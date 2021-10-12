using NetCoreBoilerplate.Application.Common.Mediator;

namespace NetCoreBoilerplate.Application.User.VerifyAccount
{
    public class VerifyAccountCommand : ICommand<VoidResult>
    {
        public string VerificationToken { get; set; }
    }
}
