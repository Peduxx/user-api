namespace Aplicativo.DTOs
{
    public class SignInDTO
    {
        public string Token { get; set; }

        public SignInDTO(string token)
        {
            Token = token;
        }
    }
}