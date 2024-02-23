using Domain.Entities;

namespace Domain.Repositories.Ports
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task Activate(Guid userId);
    }
}