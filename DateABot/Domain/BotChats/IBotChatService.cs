namespace Domain.BotChats
{
    public interface IBotChatService
    {
        Task<string> Send(BotChatDto botChat, string message);
    }
}
