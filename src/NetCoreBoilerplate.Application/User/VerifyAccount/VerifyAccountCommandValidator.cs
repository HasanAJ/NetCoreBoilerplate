using FluentValidation;

namespace NetCoreBoilerplate.Application.User.VerifyAccount
{
    public class VerifyAccountCommandValidator : AbstractValidator<VerifyAccountCommand>
    {
        public VerifyAccountCommandValidator()
        {
            RuleFor(v => v.VerificationToken)
                .NotEmpty()
                .MaximumLength(128);
        }
    }
}
