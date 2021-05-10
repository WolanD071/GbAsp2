//using System;
//using Serilog;
//using Serilog.Events;
//using Serilog.Formatting.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GbWebApp.ServiceHosting
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(host => host.UseStartup<Startup>())
            //.UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)   // log4net will NOT work together with serilog
            ////    .MinimumLevel.Debug()
            ////    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            ////    .Enrich.FromLogContext()
            ////    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
            ////    .WriteTo.RollingFile($@".\Log\GbWebApp.ServiceHosting-[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
            ////    .WriteTo.File(new JsonFormatter(",", true), $@".\Log\GbWebApp.ServiceHosting-[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
            ////    //.WriteTo.Seq("http://localhost:5341")
            //)
        ;
    }
}
