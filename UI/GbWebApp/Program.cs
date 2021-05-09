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
                    host.UseStartup<Startup>());
        }
    }
}
