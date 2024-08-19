using Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityFramework.Configurations
{
    internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.CreatedAt);

            builder.Property(t => t.UpdatedAt);

            builder.Property(t => t.Active);

            builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Bot)
                .WithMany()
                .HasForeignKey(t => t.BotId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
