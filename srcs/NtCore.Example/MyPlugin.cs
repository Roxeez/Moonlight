using NtCore.API.Enums;
using NtCore.API.Events.Maps;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.Example
{
    [PluginInfo(IsInjected = true)]
    public class MyPlugin : Plugin
    {
        public override string Name => "MyPlugin";
        public override string Version => "1.0.0";

        
        public override void OnEnable()
        {
            PluginManager.RegisterListeners(this, new MyListener());
            Logger.Information("Successfully started");
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