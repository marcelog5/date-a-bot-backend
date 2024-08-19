using Domain.Abstracts;

namespace Domain.Chats
{
    public sealed class Chat : Entity
    {
        public Chat(
            Guid userId, 
            Guid botId)
        {
            if (userId == Guid.Empty)
            {
                throw new Exception("User id is required");
            }
            if (botId == Guid.Empty)
            {
                throw new Exception("Bot id is required");
            }

            UserId = userId;
            BotId = botId;
        }

        public Guid UserId { get; private set; }
        public Guid BotId { get; private set; }
    }
}
