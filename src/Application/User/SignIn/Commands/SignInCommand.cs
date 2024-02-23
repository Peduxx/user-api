namespace Application.User.SignIn.Commands
{
    public class SignInCommand : IRequest<Response>
    {
        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}