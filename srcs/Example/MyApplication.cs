using System;
using NtCore;
using NtCore.Clients;
using NtCore.Commands;
using NtCore.Enums;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Events.Entity;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Import;

namespace Example
{
    public class MyApplication
    {
        public void Run()
        {
            // Alloc console (if needed)
            Kernel32.AllocConsole();

            // Create a new local client (for injected dll)
            NtCoreAPI.Instance.CreateLocalClient();
            
            // Register our event listener
            NtCoreAPI.Instance.RegisterEventListener<MyEventListener>();
            
            // Register our command handler
            NtCoreAPI.Instance.RegisterCommandHandler<MyCommandHandler>();

            // Wait for exit command (because i'm not using a UI application, only console)
            string command;
            do
            {
                command = Console.ReadLine();
            } 
            while (command != "exit");
        }
    }

    public class MyCommandHandler : ICommandHandler
    {
        [Command("ping")]
        public void OnPingCommand(ICharacter sender)
        {
            sender.ShowChatMessage("pong", ChatMessageColor.GREEN);
        }
    }

    public class MyEventListener : IEventListener
    {
        [Handler]
        public void OnTargetMove(TargetMoveEvent e)
        {
            ICharacter character = e.Character;
            ILivingEntity target = character.Target.Entity;

            character.Move(target.Position);
            character.ShowBubbleMessage($"Following {target.EntityType}:{target.Id}");
        }

        [Handler]
        public void OnEntitySpawn(EntityJoinEvent e)
        {
            if (e.Entity.EntityType == EntityType.PLAYER)
            {
                var player = e.Entity.As<IPlayer>();

                foreach (IClient client in NtCoreAPI.Instance.Clients)
                {
                    client.Character.ShowChatMessage($"{player.Name} / Lv.{player.Level} joined map {e.Map.Id}", ChatMessageColor.YELLOW);
                }
            }
        }
    }
}