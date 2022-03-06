using Listening.Infrastructure;
using Listening.Server.Services.Contracts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Listening.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
                    var textService = services.GetRequiredService<ITextService>();
                    await databaseInitializer.SeedAsync(Startup.Configuration);
                    await textService.ResaveAndRecalculateAllTexts();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical("Error creating/seeding database - " + ex);
                }
            }

            //Console.WriteLine("barada");
            Console.WriteLine(Process.GetCurrentProcess().Id);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                  WebHost.CreateDefaultBuilder(args)
                        //.UseKestrel(options =>
                        //{
                        //    var portString = Startup.Configuration["HostUrl"].Split(':').Last().Replace("/", "");

                        //    options.Listen(IPAddress.Loopback, 5000);
                        //    options.Listen(IPAddress.Loopback, Convert.ToInt32(portString), listenOptions =>
                        //    {
                        //        listenOptions.UseHttps(Startup.Configuration["CertificatePath"],
                        //            Startup.Configuration["CertificatePasswd"]);
                        //    });
                        //})
                        //.UseSetting("https_port", "8080")
                        .UseStartup<Startup>();
    }
}
