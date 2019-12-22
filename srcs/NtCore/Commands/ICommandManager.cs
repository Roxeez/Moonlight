using JetBrains.Annotations;
using NtCore.Clients;

namespace NtCore.Commands
{
    /// <summary>
    ///     Used for managing all plugins commands
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        ///     Register a command handler
        /// </summary>
        /// <param name="handler">Command handler</param>
        void RegisterCommandHandler([NotNull] ICommandHandler handler);

        /// <summary>
        ///     Register a command handler
        /// </summary>
        /// <typeparam name="T">Type implementing ICommandHandler</typeparam>
        void RegisterCommandHandler<T>() where T : ICommandHandler;

        /// <summary>
        ///     Execute a command
        /// </summary>
        /// <param name="client">Client who used this command</param>
        /// <param name="command">Command to execute</param>
        /// <param name="args">Command arguments</param>
        bool ExecuteCommand([NotNull] IClient client, [NotNull] string command, [NotNull] string[] args);
    }
}