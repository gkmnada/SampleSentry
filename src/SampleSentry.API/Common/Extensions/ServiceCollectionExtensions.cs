using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SampleSentry.API.Features.ApplicationUser.Validators;
using SampleSentry.API.Features.Category.Validators;
using SampleSentry.API.Models;
using SampleSentry.API.Repositories.Category;
using SampleSentry.API.Tools;
using System.Reflection;
using System.Text;

namespace SampleSentry.API.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<CreateCategoryValidator>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<CreateApplicationUserValidator>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var defaults = new JwtTokenDefaults();

            configuration.Bind("JwtTokenDefaults", defaults);
            services.Configure<JwtTokenDefaults>(configuration.GetSection("JwtTokenDefaults"));

            services.AddSingleton<JwtTokenGenerator>();

            var key = Encoding.UTF8.GetBytes(defaults.SecretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = defaults.ValidIssuer,
                        ValidAudience = defaults.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };

                    options.MapInboundClaims = false;
                });

            return services;
        }
    }
}
