using BCP.Application.Interfaces.Common;
using BCP.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BCP.Infraestructure
{
    public static class ConfigureServices
    {
        public static void AddInfraestructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("BcpDatabase"));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();
        }
    }
}