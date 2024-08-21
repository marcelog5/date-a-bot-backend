using Application.UseCases.BotUseCases;
using Application.UseCases.ChatUseCases;
using Application.UseCases.UserUseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ICreateChatUseCase, CreateChatUseCase>();
            services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
            services.AddTransient<ICreateBotUseCase, CreateBotUseCase>();

            return services;
        }
    }
}
