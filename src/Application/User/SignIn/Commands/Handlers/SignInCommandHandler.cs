using Aplicativo.Abstractions;
using Application.Abstractions;
using Domain.Repositories.Ports;

namespace Application.User.SignIn.Commands.Handlers
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordProvider _passwordProvider;
        private readonly SignInCommandValidator _validator;

        public SignInCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordRepository passwordRepository, IPasswordProvider passwordProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _passwordRepository = passwordRepository;
            _passwordProvider = passwordProvider;
            _validator = new SignInCommandValidator();
        }

        public async Task<Response> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new Response().SetFailure(validationResult.Errors.FirstOrDefault().ErrorMessage);
            }

            var user = await _userRepository.GetByEmailAsync(request.Email);

            if(user == null)
                return new Response().SetFailure("Your email or password is incorrect.");

            if(!user.IsActive)
                return new Response().SetFailure("You need to activate you account. Check your email.");

            var password = await _passwordRepository.GetByUserId(user.Id);
            var salt = await _passwordRepository.GetSaltByUserId(user.Id);

            var checkPassword = _passwordProvider.Compare(request.Password, password, salt);

            if (!checkPassword)
                return new Response().SetFailure("Your email or password is incorrect.");

            var token = _jwtProvider.Generate(user);

            return new Response().SetSuccess(new SignInDTO(token));
        }
    }
}