using Hookah.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hookah
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    //                    context.Database.Migrate();
                    context.Database.Migrate();
                    SeedData.Initialize(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder
                     .ConfigureKestrel(m => {
                         m.Limits.MaxRequestBodySize = 1048576000;
                         m.Limits.MaxRequestBufferSize = 20000000;
                         m.Limits.MaxRequestHeadersTotalSize = 20000000;
                     })
                         .UseStartup<Startup>()
                         .ConfigureLogging(logging =>
                         {
                             logging.ClearProviders();
                             logging.AddConsole();
                         });
                 });
    }
}