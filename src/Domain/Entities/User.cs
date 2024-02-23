using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Password Password { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        private User() { }

        public static User Create(string name, string email, DateTime birthDate)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                BirthDate = birthDate,
                IsActive = false
            };
        }
    }
}