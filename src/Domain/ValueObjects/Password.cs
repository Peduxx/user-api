namespace Domain.ValueObjects
{
    public class Password
    {
        public Password(Guid userId, string value, byte[] salt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Value = value;
            Salt = salt;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Value { get; set; }
        public byte[] Salt { get; set; }
    }
}
