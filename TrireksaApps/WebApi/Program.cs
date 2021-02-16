using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrireksaAppContext;
using WebApi.Middlewares;
using WebApi.Services;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var telegram = services.GetRequiredService<TelegramService>();
                    telegram.StartReceiving();
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<IUserService>();
                    await DbInitializer.Initialize(context, userManager);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                //IIS
                //.UseIISIntegration();
                
                //Kestrell
                .UseKestrel(options =>{options.Limits.MaxRequestBodySize = 52428800;});
                webBuilder.UseUrls("http://localhost:5004");
            });
    }

}
