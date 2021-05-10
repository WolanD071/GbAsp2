using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string cfgFilePath)
        {
            if (cfgFilePath is not { Length: > 0 })
                throw new ArgumentException("Wrong file path!", nameof(cfgFilePath));
            if (Path.IsPathRooted(cfgFilePath)) return cfgFilePath;
            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir!, cfgFilePath);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string cfgFileName = "log4net.config")
        {
            factory.AddProvider(new Log4NetLoggerProvider(CheckFilePath(cfgFileName)));
            return factory;
        }
    }
}
