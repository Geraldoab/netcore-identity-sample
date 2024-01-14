using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserAPI.Authorization;
using UserAPI.Authorization.Policies;
using UserAPI.Data;
using UserAPI.Domain.Interfaces;
using UserAPI.Filters;
using UserAPI.Models;
using UserAPI.Services;
using UserAPI.Services.Message;

namespace UserAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add serices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            var userConnectionString = Environment.GetEnvironmentVariable("USER_CONNECTIONSTRING");

            services.AddDbContext<UserDbContext>(opts =>
            {
                opts.UseMySql(userConnectionString,
                    ServerVersion.AutoDetect(userConnectionString));
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SYMMETRIC_SECURITY_KEY"))),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MinimumAge", policy =>
                {
                    policy.AddRequirements(new MinimumAge(18));
                });
            });

            services.AddScoped<UserService>();
            services.AddScoped<TokenService>();
            services.AddScoped<IEmailMessengerService, EmailMessengerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Should be only executed in development environment because can cause performance problems
            if (env.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var context = services.GetRequiredService<UserDbContext>();
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        Console.WriteLine("Executing migrations when necessary...");
                        context.Database.Migrate();
                    }
                    else
                    {
                        Console.WriteLine("We don't need to execute migrations, it is everything fine with our database...");
                    }
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
