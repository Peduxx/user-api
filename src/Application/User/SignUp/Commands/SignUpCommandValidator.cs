using FluentValidation;

namespace Application.User.SignUp.Commands
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(command => command.Email).NotEmpty().WithMessage("Email cannot be empty.")
                                              .EmailAddress().WithMessage("Invalid Email.");
            RuleFor(command => command.BirthDate).Must(BeAValidDate).WithMessage("Invalid Birth Date.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date >= new DateTime(1900, 1, 1) && date <= DateTime.Today.AddYears(-10);
        }
    }
}
