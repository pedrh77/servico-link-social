using LinkSocial_Domain.Interfaces;
using LinkSocial_Domain.Interfaces.Auth;
using LinkSocial_Domain.Interfaces.Carteiras;
using LinkSocial_Domain.Interfaces.Doacoes;
using LinkSocial_Domain.Interfaces.Pedidos;
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
            TokenInjection(services, configuration);
            DatabaseConfiguration(services, configuration);
            RegisterServices(services);
            RegisterRepositories(services);
            ConfigureCors(services);

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

            services.AddAuthorization(options =>
            {

                options.AddPolicy("ONG", policy =>
                    policy.RequireRole("ONG"));
                options.AddPolicy("Doador", policy =>
                    policy.RequireRole("Doador"));
            });

        }


        private static void DatabaseConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LinkSocialDbContext>(
                config => config.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"), b => b.MigrationsAssembly("LinkSocial-Infra"))
                );
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IDoacaoService, DoacaoService>();
            services.AddScoped<IMd5HashService, Md5HashService>();
            services.AddScoped<IPedidoService, PedidoService>();

            services.AddScoped<ICarteiraService, CarteiraService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IDoacaoRepository, DoacaoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ICarteiraRepository, CarteiraRepository>();
        }


        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}