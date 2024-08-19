using Domain.Bots;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class BotConfiguration : IEntityTypeConfiguration<Bot>
    {
        public void Configure(EntityTypeBuilder<Bot> builder)
        {
            builder.ToTable("Bots");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name).
                HasMaxLength(200)
                .HasConversion(name => name.Value, value => new Name(value));

            builder.Property(t => t.Role);

            builder.Property(t => t.Goal);

            builder.Property(t => t.Backstory);

            builder.Property(t => t.Avatar);

            builder.Property(t => t.CreatedAt);

            builder.Property(t => t.UpdatedAt);

            builder.Property(t => t.Active);
        }
    }
}
