using System;
using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Events.Maps;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore.Example
{
    [PluginInfo(Name = "MyPlugin", Version = "1.0", IsInjected = true)]
    public class MyPlugin : IPlugin
    {
        private readonly ILogger _logger;
        private readonly IPluginManager _pluginManager;
        private readonly IScheduler _scheduler;
        
        public MyPlugin(ILogger logger, IPluginManager pluginManager, IScheduler scheduler)
        {
            _logger = logger;
            _pluginManager = pluginManager;
            _scheduler = scheduler;
        }
        
        public void OnStart()
        {
            _pluginManager.Register(new MyListener(_scheduler));
            _logger.Information("[MyPlugin] Successfully started");
        }
    }

    public class MyListener : IListener
    {
        private readonly IScheduler _scheduler;
        
        public MyListener(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }
        
        [Handler]
        public void OnEntitySpawn(EntitySpawnEvent e)
        {
            if (e.Entity.EntityType == EntityType.MONSTER)
            {
                IMonster monster = e.Entity.As<IMonster>();
            }
        }

        [Handler]
        public void OnMapChange(MapChangeEvent e)
        {
            _scheduler.Schedule(TimeSpan.FromSeconds(1), () =>
            {
                
            });
        }
    }
}