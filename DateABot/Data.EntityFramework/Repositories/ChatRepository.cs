using Domain.Chats;
using Microsoft.EntityFrameworkCore;

namespace Data.EntityFramework.Repositories
{
    internal sealed class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> Exist(Guid userId, Guid botId, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Chat>()
                .AnyAsync(chat => chat.UserId == userId && chat.BotId == botId, cancellationToken);
        }
    }
}
