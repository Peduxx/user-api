namespace Application.User.SignUp.Commands
{
    public class SignUpCommand : IRequest<Response>
    {
        public SignUpCommand(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
