using NtCore.API;
using NtCore.API.Commands;
using NtCore.API.Enums;
using NtCore.API.Events.Character;
using NtCore.API.Events.Entity;
using NtCore.API.Extensions;
using NtCore.API.Game.Battle;
using NtCore.API.Game.Entities;
using NtCore.API.Plugins;

namespace NtCore.Example
{
    [PluginInfo(NeedInjection = true)]
    public class MyPlugin : Plugin
    {
        public override string Name => "MyPlugin";
        public override string Version => "1.0.0";

        public override void OnEnable()
        {
            NtCoreAPI.GetPluginManager().Register<MyListener>(this);
            NtCoreAPI.GetCommandManager().Register<MyCommandHandler>(this);
        }
    }

    public class MyCommandHandler : ICommandHandler
    {
        [Command("ping")]
        public void OnPingCommand(ICharacter sender)
        {
            sender.ShowMessage("pong", MessageType.MIDDLE_SCREEN);
        }

        [Command("echo")]
        public void OnEchoCommand(ICharacter sender, string[] args)
        {
            sender.ShowBubbleMessage(string.Join(" ", args));
        }
    }

    public class MyListener : IListener
    {
        [Handler]
        public void OnEntitySpawn(EntitySpawnEvent e)
        {
            if (e.Entity.EntityType == EntityType.PLAYER)
            {
                var player = e.Entity.As<IPlayer>();

                e.Character.ShowChatMessage($"{player.Id}/{player.Name}/{player.Level}", ChatMessageType.RED);
            }
        }

        [Handler]
        public void OnTargetChange(TargetChangeEvent e)
        {
            ITarget target = e.Target;
            ILivingEntity entity = e.Target.Entity;

            e.Character.ShowBubbleMessage($"Id {entity.Id} / Type {entity.EntityType} / Level {entity.Level} / Hp {target.Hp} / Mp {target.Mp}", entity);
        }
    }
}