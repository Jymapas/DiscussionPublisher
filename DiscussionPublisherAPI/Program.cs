using System.Security.Cryptography.X509Certificates;
using DiscussionPublisher.TelegramBot;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace DiscussionPublisherAPI;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("test");
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Warning)
            .WriteTo.File(
                path: "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10_000_000,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

        var host = CreateHostBuilder(args).Build();

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                    .UseUrls("http://*:5000", "https://*:5001")
                    .UseKestrel(options =>
                    {
                        options.ListenAnyIP(5000, o => o.Protocols = HttpProtocols.Http1AndHttp2);
                        options.ListenAnyIP(5001, o =>
                        {
                            o.Protocols = HttpProtocols.Http1AndHttp2;
                            o.UseHttps(httpsOptions =>
                            {
                                httpsOptions.ServerCertificate = new X509Certificate2(Path.Combine(AppContext.BaseDirectory, "Certificate/certificate.pfx"), Keys.CertificatePassword);
                            });
                        });
                    });
            })
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });
}
