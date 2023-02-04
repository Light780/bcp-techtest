using System.Reflection;
using BCP.Application.Interfaces.Security;
using BCP.Application.Interfaces.Services;
using BCP.Application.Interfaces.Services.Moneda;
using BCP.Application.Security;
using BCP.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Application
{
    public static class ConfigureServices
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IMonedaService, MonedaService>();
            services.AddScoped<ITipoCambioService, TipoCambioService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUsuarioSession, UsuarioSession>();
        }
    }
}