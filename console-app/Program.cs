using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace console_app
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      var builder = new ConfigurationBuilder();
      BuildConfig(builder);
      Log.Logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Build())
      .Enrich.FromLogContext()
      .WriteTo.Console()
      .CreateLogger();

      var host = Host.CreateDefaultBuilder()
      .ConfigureServices((context, services) =>
      {
        // add services here
        services.AddTransient<GreetingService>();
      })
      .UseSerilog()
      .Build();

      var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
      svc.Run();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
      // sets up talking to configuration source
      builder.SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile(
          $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
          optional: true)
      .AddEnvironmentVariables();
    }
  }

  public class GreetingService
  {
    private readonly ILogger<GreetingService> _log;
    private readonly IConfiguration _config;

    public GreetingService(ILogger<GreetingService> log, IConfiguration config)
    {
      _log = log;
      _config = config;
    }

    public void Run()
    {
      for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
      {
        _log.LogInformation("Run number {runNumber}", i);
      }
    }
  }
}
