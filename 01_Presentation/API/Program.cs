using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(settings["Elasticsearch:Uri"]))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            IndexFormat = settings["Elasticsearch:IndexName"]+"-{0:yyyy.MM}",
                            ModifyConnectionSettings = x => x.BasicAuthentication(settings["Elasticsearch:User"], settings["Elasticsearch:Pass"])
                        })
                        .WriteTo.Console()
                        .CreateLogger();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
