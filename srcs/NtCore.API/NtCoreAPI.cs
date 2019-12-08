using System;
using NtCore.API.Clients;
using NtCore.API.Commands;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore.API
{
    /// <summary>
    ///     Static class used for accessing all initialized managers created by NtCore
    /// </summary>
    public static class NtCoreAPI
    {
        private static INtCore _ntCore;

        public static void Initialize(INtCore ntCore)
        {
            if (_ntCore != null)
            {
                throw new InvalidOperationException();
            }

            _ntCore = ntCore;
        }

        /// <summary>
        ///     Get scheduler
        /// </summary>
        /// <returns>Scheduler</returns>
        public static IScheduler GetScheduler() => _ntCore.Scheduler;

        /// <summary>
        ///     Get plugin manager
        /// </summary>
        /// <returns>PluginManager</returns>
        public static IPluginManager GetPluginManager() => _ntCore.PluginManager;

        /// <summary>
        ///     Get global logger
        /// </summary>
        /// <returns>Logger</returns>
        public static ILogger GetLogger() => _ntCore.Logger;

        /// <summary>
        ///     Get command manager
        /// </summary>
        /// <returns>Command manager</returns>
        public static ICommandManager GetCommandManager() => _ntCore.CommandManager;

        /// <summary>
        ///     Get client manager
        /// </summary>
        /// <returns>Client manager</returns>
        public static IClientManager GetClientManager() => _ntCore.ClientManager;
    }
}