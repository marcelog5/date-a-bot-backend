using Bot.Http.Services;
using Domain.BotChats;
using Microsoft.Extensions.DependencyInjection;

namespace Bot.Http
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBotConnection(this IServiceCollection services)
        {
            services.AddHttpClient<IBotChatService, BotChatService>();

            return services;
        }
    }
}
