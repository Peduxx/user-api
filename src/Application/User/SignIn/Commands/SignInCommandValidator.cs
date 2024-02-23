using FluentValidation;

namespace Application.User.SignIn.Commands
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(command => command.Email).NotEmpty().WithMessage("Email cannot be empty.")
                                              .EmailAddress().WithMessage("Invalid Email.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("The password cannot be empty.");
        }
    }
}
