namespace Application.UseCases.ChatUseCases
{
    public record CreateChatInput(
        Guid UserId,
        Guid BotId);
}
