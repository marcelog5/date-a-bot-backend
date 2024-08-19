namespace Domain.Chats
{
    public interface IChatRepository
    {
        Task<bool> Exist(Guid userId, Guid botId, CancellationToken cancellationToken = default);
        Task Add(Chat chat, CancellationToken cancellationToken = default);
    }
}
