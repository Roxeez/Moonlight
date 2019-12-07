using System;
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
            if (_ntCore != null) throw new InvalidOperationException();
            _ntCore = ntCore;
        }

        public static IScheduler GetScheduler()
        {
            return _ntCore.Scheduler;
        }

        public static IPluginManager GetPluginManager()
        {
            return _ntCore.PluginManager;
        }

        public static ILogger GetLogger()
        {
            return _ntCore.Logger;
        }
    }
}