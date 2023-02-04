using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCP.Infraestructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BCP.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostServer = CreateHostBuilder(args).Build();
            using (var scope = hostServer.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var initialiser = services.GetRequiredService<ApplicationDbContextInitialiser>();
                    initialiser.SeedAsync().Wait();
                }
                catch(Exception e)
                {
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(e, "Error while seeding data");
                }
            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}