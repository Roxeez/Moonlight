using JetBrains.Annotations;

namespace NtCore.Logger
{
    /// <summary>
    ///     Interface used for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Log debug message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Debug([NotNull] string message);

        /// <summary>
        ///     Log info message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Information([NotNull] string message);

        /// <summary>
        ///     Log warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Warning([NotNull] string message);

        /// <summary>
        ///     Log error message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Error([NotNull] string message);
    }
}