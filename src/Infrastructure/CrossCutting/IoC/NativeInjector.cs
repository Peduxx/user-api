using Aplicativo.Abstractions;
using Application.Abstractions;
using Domain.Repositories.Ports;
using Infraestrutura.Authentication;
using Infrastructure.Password;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Infrastructure - Providers
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordProvider, PasswordProvider>();

            // Persistence - Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
        }
    }
}
