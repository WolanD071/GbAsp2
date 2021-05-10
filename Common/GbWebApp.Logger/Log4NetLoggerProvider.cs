using System.Xml;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace GbWebApp.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _cfgFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new();

        public Log4NetLoggerProvider(string cfgFile) => _cfgFile = cfgFile;

        public ILogger CreateLogger(string category) =>
            _loggers.GetOrAdd(category, cat =>
            {
                var xml = new XmlDocument();
                xml.Load(_cfgFile);
                return new Log4NetLogger(cat, xml["log4net"]);
            });

        public void Dispose() => _loggers.Clear();
    }
}
