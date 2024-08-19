using Domain.Shared;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).
                HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.Property(t => t.Email).
                HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Email(value));

            builder.Property(t => t.Password).
                HasMaxLength(400);

            builder.Property(t => t.CreatedAt);

            builder.Property(t => t.UpdatedAt);

            builder.Property(t => t.Active);
        }
    }
}
