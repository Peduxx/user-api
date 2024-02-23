using Domain.Entities;
using Domain.Repositories.Ports;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public async Task Activate(Guid userId)
        {
            var user = await _context.User.FindAsync(userId);
            user!.IsActive = true;
            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
