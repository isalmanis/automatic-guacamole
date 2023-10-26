using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Guacamole.Server;
using Microsoft.AspNetCore;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((builder, config) =>
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("KeyVaultUri")))
                    config = config.AddAzureKeyVault(new Uri(Environment.GetEnvironmentVariable("KeyVaultUri") 
                                                             ?? throw new ArgumentNullException()),
                        new DefaultAzureCredential(), new AzureKeyVaultConfigurationOptions
                        {
                            ReloadInterval = TimeSpan.FromMinutes(20)
                        });
                config = config.AddJsonFile("local.settings.json", true, true);
                config.Build();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddFile("logs/{Date}.log");
                logging.AddConsole();
            })
            .UseStartup<Startup>();
}