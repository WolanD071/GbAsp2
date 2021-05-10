using System;
using log4net;
using System.Xml;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GbWebApp.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string category, XmlElement configuration)
        {
            var loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(loggerRepository.Name, category);

            log4net.Config.XmlConfigurator.Configure(loggerRepository, configuration);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentOutOfRangeException(nameof(formatter));

            var logMessage = formatter(state, exception);

            if (string.IsNullOrEmpty(logMessage) && exception is null) return;

            switch (logLevel)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
                case LogLevel.None:
                    break;
                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(logMessage);
                    break;
                case LogLevel.Information:
                    _log.Info(logMessage);
                    break;
                case LogLevel.Warning:
                    _log.Warn(logMessage);
                    break;
                case LogLevel.Error:
                    _log.Error(logMessage, exception);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(logMessage, exception);
                    break;
            }
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _log.IsDebugEnabled,
            LogLevel.Debug => _log.IsDebugEnabled,
            LogLevel.Information => _log.IsInfoEnabled,
            LogLevel.Warning => _log.IsWarnEnabled,
            LogLevel.Error => _log.IsErrorEnabled,
            LogLevel.Critical => _log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(LogLevel), logLevel, null)
        };

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}
