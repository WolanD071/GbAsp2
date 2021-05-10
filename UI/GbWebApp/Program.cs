//using System;
using Serilog;
//using Serilog.Events;
//using Serilog.Formatting.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;

namespace GbWebApp
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            //var logOptions = new ConsoleFormatterOptions();

            return Host.CreateDefaultBuilder(args)
                //.ConfigureLogging((host, log) => log    // not preferred way - use appsettings.json instead
                //    .ClearProviders()
                //    //.AddConsole(opt =>
                //    //    {                             // deprecated
                //    //        opt.IncludeScopes = true;
                //    //        opt.TimestampFormat = "HH:mm:ss ";
                //    //        opt.UseUtcTimestamp = true;
                //    //    }
                //    //    //{                           // well, but does not work!
                //    //    //    logOptions.IncludeScopes = true;
                //    //    //    logOptions.TimestampFormat = "HH:mm:ss ";
                //    //    //}
                //    //)
                //    .AddFilter /*<ConsoleLoggerProvider>*/("Microsoft.Hosting", LogLevel.Error)
                //)
                .ConfigureWebHostDefaults(host =>
                    host.UseStartup<Startup>())
                .UseSerilog((host, log) => log.ReadFrom.Configuration(host.Configuration)   // log4net will NOT work together with serilog
                //    .MinimumLevel.Debug()
                //    .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                //    .Enrich.FromLogContext()
                //    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}")
                //    .WriteTo.RollingFile($@".\Log\GbWebApp-[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log")
                //    .WriteTo.File(new JsonFormatter(",", true), $@".\Log\GbWebApp-[{DateTime.Now:yyyy-MM-ddTHH-mm-ss}].log.json")
                //    //.WriteTo.Seq("http://localhost:5341")
                )
            ;
        }
    }
}
