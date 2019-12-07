using System;
using NtCore.API.Commands;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore.API
{
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

        public static IScheduler GetScheduler() => _ntCore.Scheduler;

        public static IPluginManager GetPluginManager() => _ntCore.PluginManager;

        public static ILogger GetLogger() => _ntCore.Logger;

        public static ICommandManager GetCommandManager() => _ntCore.CommandManager;
    }
}