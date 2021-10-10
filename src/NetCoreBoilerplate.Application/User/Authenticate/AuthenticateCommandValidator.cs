using FluentValidation;

namespace NetCoreBoilerplate.Application.User.Authenticate
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty()
                .MaximumLength(64)
                .EmailAddress();

            RuleFor(v => v.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(128);
        }
    }
}
