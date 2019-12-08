using JetBrains.Annotations;
using NtCore.API.Clients;
using NtCore.API.Plugins;

namespace NtCore.API.Commands
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
        /// <param name="plugin">Plugin instance</param>
        void Register([NotNull] ICommandHandler handler, [NotNull] Plugin plugin);

        /// <summary>
        ///     Register a command handler
        /// </summary>
        /// <param name="plugin">Plugin instance</param>
        /// <typeparam name="T">Type implementing ICommandHandler</typeparam>
        void Register<T>([NotNull] Plugin plugin) where T : ICommandHandler;

        /// <summary>
        ///     Execute a command
        /// </summary>
        /// <param name="client">Client who used this command</param>
        /// <param name="command">Command to execute</param>
        /// <param name="args">Command arguments</param>
        void Execute([NotNull] IClient client, [NotNull] string command, [NotNull] string[] args);
    }
}