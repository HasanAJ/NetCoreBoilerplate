using FluentValidation;

namespace NetCoreBoilerplate.Application.User.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
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
