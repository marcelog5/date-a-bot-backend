using Data.EntityFramework.Repositories;
using Domain.Abstracts;
using Domain.Bots;
using Domain.Chats;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.EntityFramework
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataEfCore(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString =
                            configuration.GetConnectionString("Database") ??
                            throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBotRepository, BotRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
