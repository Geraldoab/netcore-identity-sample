using UserAPI.Application.Services;
using UserAPI.Domain.Interfaces.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
