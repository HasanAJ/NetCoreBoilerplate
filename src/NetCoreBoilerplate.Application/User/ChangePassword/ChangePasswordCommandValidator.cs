using FluentValidation;

namespace NetCoreBoilerplate.Application.User.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(v => v.OldPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);

            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);

            RuleFor(v => v)
                .Must(IsNewPasswordDifferent)
                .WithMessage("New password should be different from old password");
        }

        private bool IsNewPasswordDifferent(ChangePasswordCommand d)
        {
            return (d.OldPassword != d.NewPassword);
        }
    }
}
