using Domain.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Security.PasswordHashing
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHashing(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            return services;
        }
    }
}
