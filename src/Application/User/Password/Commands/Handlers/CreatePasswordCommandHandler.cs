using Application.Abstractions;
using Domain.Repositories.Ports;

namespace Application.User.Password.Commands.Handlers
{
    public class CreatePasswordCommandHandler : IRequestHandler<CreatePasswordCommand, Response>
    {
        private readonly IPasswordProvider _passwordProvider;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IUserRepository _userRepository;
        private readonly CreatePasswordCommandValidator _validator;

        public CreatePasswordCommandHandler(IPasswordProvider passwordProvider, IPasswordRepository passwordRepository, IUserRepository userRepository)
        {
            _passwordProvider = passwordProvider;
            _passwordRepository = passwordRepository;
            _userRepository = userRepository;
            _validator = new CreatePasswordCommandValidator();
        }

        public async Task<Response> Handle(CreatePasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new Response().SetFailure(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var salt = _passwordProvider.GenerateSalt();

            var hashedPassword = _passwordProvider.Generate(request.Password, salt);

            await _passwordRepository.Save(new PasswordObject(request.UserId, hashedPassword, salt));

            await _userRepository.Activate(request.UserId);

            return new Response().SetSuccess();
        }
    }
}
