using Application.DTOs;
using Application.User.SignUp.Commands;
using Domain.Repositories.Ports;

namespace Application.User.SignUp.Command.Handlers
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly SignUpCommandValidator _validator;

        public SignUpCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _validator = new SignUpCommandValidator();
        }

        public async Task<Response> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new Response().SetFailure(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user != null)
                return new Response().SetFailure("This email already exists.");

            user = UserEntity.Create(request.Name, request.Email, request.BirthDate);

            await _userRepository.CreateAsync(user);

            return new Response().SetSuccess(new SignUpDTO(user.Id), "User successfully created.");
        }
    }
}
