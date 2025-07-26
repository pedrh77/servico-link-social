using LinkSocial_Domain.Interfaces.Auth;
using LinkSocial_Domain.Interfaces.Usuarios;
using LinkSocial_Domain.Services;
using LinkSocial_Infra.Contexts;
using LinkSocial_Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkSocial_IoC
{
    public static class BootStraper
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            DatabaseConfiguration(services, configuration);

            RegisterServices(services);
            RegisterRepositories(services);
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
                config => config.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"), b => b.MigrationsAssembly("LinkSocial_Infra"))
                );
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

    }
}
