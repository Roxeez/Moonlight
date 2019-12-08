using System;

namespace NtCore.API.Logger
{
    /// <summary>
    ///     Class used for logging in console
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        private readonly string _prefix;

        /// <summary>
        ///     Create new console logger with NtCore prefix
        /// </summary>
        public ConsoleLogger() : this("NtCore")
        {
        }

        /// <summary>
        ///     Create a new console logger instance with selected prefix
        /// </summary>
        /// <param name="prefix">Logger prefix</param>
        public ConsoleLogger(string prefix) => _prefix = prefix;

        public void Debug(string message)
        {
            Console.WriteLine($"[{_prefix}][DEBUG] {message}");
        }

        public void Information(string message)
        {
            Console.WriteLine($"[{_prefix}][INFO] {message}");
        }

        public void Warning(string message)
        {
            Console.WriteLine($"[WARN][{_prefix}][WARN] {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"[{_prefix}][ERROR] {message}");
        }
    }
}