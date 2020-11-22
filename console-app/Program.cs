using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
      })
      .UseSerilog()
      .Build();
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
}
