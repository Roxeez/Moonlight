using NtCore.API.Core;
using NtCore.API.Enums;
using NtCore.API.Events.Maps;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Logger;
using NtCore.API.Plugins;

namespace NtCore.Example
{
    [PluginInfo(Name = "MyPlugin", Version = "1.0", IsInjected = true)]
    public class MyPlugin : IPlugin
    {
        private readonly ILogger _logger;
        private readonly IPluginManager _pluginManager;

        public MyPlugin(ILogger logger, IPluginManager pluginManager)
        {
            _logger = logger;
            _pluginManager = pluginManager;
        }
        
        public void OnStart()
        {
            _pluginManager.Register(new MyListener());
            _logger.Information("[MyPlugin] Successfully started");
        }
    }

    public class MyListener : IListener
    {
        [Handler]
        public void OnEntitySpawn(EntitySpawnEvent e)
        {
            var character = e.Client.Character;
            
            if (e.Entity.EntityType == EntityType.PLAYER)
            {
                IPlayer player = e.Entity.As<IPlayer>();
                
                character.ShowChatMessage($"{player.Id}/{player.Name}/{player.Level}", ChatMessageType.RED);
            }
        }
    }
}