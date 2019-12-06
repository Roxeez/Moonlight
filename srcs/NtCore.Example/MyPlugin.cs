using System.Linq;
using System.Threading.Tasks;
using NtCore.API;
using NtCore.API.Enums;
using NtCore.API.Events.Maps;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;

namespace NtCore.Example
{
    [PluginInfo(Name = "MyPlugin", Version = "1.0", IsInjected = true)]
    public class MyPlugin : Plugin, IListener
    {
        public override void Run()
        {
            RegisterListeners(this);
        }

        [EventHandler]
        public void OnMapChange(MapChangeEvent e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                
                Logger.Information("Map changed");
                
                e.Client.Communication.ReceiveChatMessage($"Monsters : {e.Destination.Monsters.Count()}", ChatMessageType.LIGHT_PURPLE);
                e.Client.Communication.ReceiveChatMessage($"Npcs : {e.Destination.Npcs.Count()}", ChatMessageType.LIGHT_PURPLE);
                e.Client.Communication.ReceiveChatMessage($"Drops : {e.Destination.Drops.Count()}", ChatMessageType.LIGHT_PURPLE);
            });
        }

        public MyPlugin(IPluginManager pluginManager, IScheduler scheduler, ILogger logger) : base(pluginManager, scheduler, logger) { }
    }
}