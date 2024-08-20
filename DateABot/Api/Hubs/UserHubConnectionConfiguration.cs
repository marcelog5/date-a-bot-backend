using Domain.BotChats;

namespace Api.Hubs
{
    public record UserHubConnectionConfiguration(
    Guid UserId,
    Guid BotId,
    string ConnectionId,
    BotChatDto BotChatDto);
}
