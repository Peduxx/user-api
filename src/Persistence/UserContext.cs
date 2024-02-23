using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;
using UserEntity = Domain.Entities.User;

namespace Persistence
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<Password> Password { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserPasswordConfiguration());
        }
    }
}
