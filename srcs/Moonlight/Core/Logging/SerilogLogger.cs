using System;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Moonlight.Core.Logging
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogger()
        {
            _logger = new LoggerConfiguration()
#if(DEBUG)
                .MinimumLevel.Debug()
#endif
#if(RELEASE)
                .MinimumLevel.Information()
#endif
                .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: "[{Timestamp:HH:mm:ss}][{Level:u4}] {Message:lj} {NewLine}{Exception}")
                .CreateLogger();
        }

        public void Debug(object message)
        {
            _logger.Debug($"{message}");
        }

        public void Info(object message)
        {
            _logger.Information($"{message}");
        }

        public void Warn(object message)
        {
            _logger.Warning($"{message}");
        }

        public void Error(object message)
        {
            _logger.Error($"{message}");
        }

        public void Error(object message, Exception ex)
        {
            _logger.Error($"{message}", ex);
        }

        public void Fatal(object message, Exception ex)
        {
            _logger.Fatal($"{message}", ex);
        }
    }
}