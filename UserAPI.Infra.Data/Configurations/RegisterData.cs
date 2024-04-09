using Microsoft.EntityFrameworkCore;
using UserAPI.Infra.Data.Context;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RegisterData
    {
        public static IServiceCollection RegisterDataDependencies(this IServiceCollection services)
        {
            var userConnectionString = Environment.GetEnvironmentVariable("USER_CONNECTIONSTRING");

            services.AddDbContext<UserDbContext>(opts =>
            {
                opts.UseMySql(userConnectionString,
                    ServerVersion.AutoDetect(userConnectionString));
            });

            return services;
        }
    }
}
