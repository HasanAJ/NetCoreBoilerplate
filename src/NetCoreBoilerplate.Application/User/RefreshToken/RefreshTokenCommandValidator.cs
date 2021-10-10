using FluentValidation;

namespace NetCoreBoilerplate.Application.User.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(v => v.RefreshToken)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}
