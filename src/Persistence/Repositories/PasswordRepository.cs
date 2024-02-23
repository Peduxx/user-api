using Domain.Repositories.Ports;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly UserContext _context;

        public PasswordRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<string> GetByUserId(Guid userId)
        {
            var password = await _context.Password.FirstOrDefaultAsync(p => p.UserId == userId);

            return password!.Value;
        }

        public async Task<byte[]> GetSaltByUserId(Guid userId)
        {
            var password = await _context.Password.FirstOrDefaultAsync(p => p.UserId == userId);

            return password.Salt;
        }

        public async Task Save(Password password)
        {
            _context.Password.Add(password);
            await _context.SaveChangesAsync();
        }
    }
}
