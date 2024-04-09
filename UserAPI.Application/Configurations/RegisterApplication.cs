using UserAPI.Application.Services;
using UserAPI.Application.Services.Message;
using UserAPI.Domain.Interfaces.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterApplication
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmailMessengerService, EmailMessengerService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            
            return services;
        }
    }
}
