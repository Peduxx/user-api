using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfigurations
{
    public class UserPasswordConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(u => u.Password)
                .WithOne()
                .HasForeignKey<Password>(p => p.UserId);
        }
    }
}
