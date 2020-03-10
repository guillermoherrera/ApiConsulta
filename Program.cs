using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Net;

namespace WebApi
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Iniciado API...");
                CreateHostBuilder(args, configuration).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "El inicio del API fallo...");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseUrls("http://0.0.0.0:3000", "https://0.0.0.0:3001");
                        //("http://0.0.0.0:4000");
                        //.UseKestrel(x =>
                        //{
                        //    x.Listen(IPAddress.Any, Convert.ToInt32(configuration["PortHttp"]));
                        //    x.Listen(IPAddress.Any, Convert.ToInt32(configuration["PortHttps"]), y =>
                        //    {
                        //        y.UseHttps(
                        //            Path.Combine(AppContext.BaseDirectory, configuration["CertificatePath"]),
                        //            configuration["CertificatePassword"]);
                        //    });
                        //});

                });
    }
}