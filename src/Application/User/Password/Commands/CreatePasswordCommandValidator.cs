using FluentValidation;

namespace Application.User.Password.Commands
{
    public class CreatePasswordCommandValidator : AbstractValidator<CreatePasswordCommand>
    {
        public CreatePasswordCommandValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("The password cannot be empty.");
        }
    }
}
