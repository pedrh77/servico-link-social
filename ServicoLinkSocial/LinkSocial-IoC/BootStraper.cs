using LinkSocial_Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkSocial_IoC
{
    public static class BootStraper
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }


        private static void TokenInjection(IServiceCollection services, IConfiguration configuration)
        {



            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Token inválido: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token válido para: " + context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    }
                };
            });

            //services.AddAuthorization(options =>
            //{

            //    options.AddPolicy("xxx", policy =>
            //        policy.RequireRole("xxx"));
            //});

        }


        private static void DatabaseConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LinkSocialDbContext>(
                config => config.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"), b => b.MigrationsAssembly("Lauerp-Infra"))
                );
        }

    }
}
