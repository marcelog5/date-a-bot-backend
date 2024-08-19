using Domain.Abstracts;

namespace Domain.Bots
{
    public static class BotErrors
    {
        public static Error NotFound = new(
            "Bot.NotFound",
            "The bot with the specified identifier could not be found.");
    }
}
