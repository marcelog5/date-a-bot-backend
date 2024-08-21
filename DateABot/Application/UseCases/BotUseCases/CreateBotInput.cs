using Domain.Shared;

namespace Application.UseCases.BotUseCases
{
    public record CreateBotInput(
        Name Name,
        string Role,
        string Goal,
        string Backstory,
        string Avatar);
}
