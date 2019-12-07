using System;
using NtCore.API;
using NtCore.API.Clients;
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
        public void OnPingCommand(IClient sender)
        {
            sender.Character.ShowMessage("pong", MessageType.MIDDLE_SCREEN);
        }

        [Command("echo")]
        public void OnEchoCommand(IClient sender, string[] args)
        {
            Console.WriteLine("Echo executed");
            sender.Character.ShowBubbleMessage(string.Join(" ", args));
        }
    }

    public class MyListener : IListener
    {
        [Handler]
        public void OnEntitySpawn(EntitySpawnEvent e)
        {
            ICharacter character = e.Client.Character;

            if (e.Entity.EntityType == EntityType.PLAYER)
            {
                IPlayer player = e.Entity.As<IPlayer>();

                character.ShowChatMessage($"{player.Id}/{player.Name}/{player.Level}", ChatMessageType.RED);
            }
        }

        [Handler]
        public void OnTargetChange(TargetChangeEvent e)
        {
            ICharacter character = e.Client.Character;
            ITarget target = e.Target;
            ILivingEntity entity = e.Target.Entity;
            
            character.ShowBubbleMessage($"Id {entity.Id} / Type {entity.EntityType} / Level {entity.Level} / Hp {target.Hp} / Mp {target.Mp}", entity);
        }
    }
}