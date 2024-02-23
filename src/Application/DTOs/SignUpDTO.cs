namespace Application.DTOs
{
    public class SignUpDTO
    {
        public Guid UserId { get; set; }

        public SignUpDTO(Guid userId)
        {
            UserId = userId;
        }
    }
}
