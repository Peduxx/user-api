namespace Application.User.Password.Commands
{
    public class CreatePasswordCommand : IRequest<Response>
    {
        public CreatePasswordCommand()
        {
        }

        public CreatePasswordCommand(Guid userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
