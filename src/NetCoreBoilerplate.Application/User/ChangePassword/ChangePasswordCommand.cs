using NetCoreBoilerplate.Application.Common.Mediator;

namespace NetCoreBoilerplate.Application.User.ChangePassword
{
    public class ChangePasswordCommand : ICommand<VoidResult>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
