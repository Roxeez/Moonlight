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
        private readonly NtCoreAPI _ntCoreApi;
        
        public MyApplication(NtCoreAPI ntCoreApi)
        {
            _ntCoreApi = ntCoreApi;
        }
        
        public void Run()
        {
            // Alloc console (if needed)
            Kernel32.AllocConsole();

            // Create a new local client (for injected dll)
            _ntCoreApi.CreateLocalClient();

            // Register listener for a specific session
            _ntCoreApi.RegisterEventListener<MySessionListener>(_ntCoreApi.LocalClient);
            
            // Register listener for all sessions
            _ntCoreApi.RegisterEventListener<MyGlobalListener>();

            // Register our command handler
            _ntCoreApi.RegisterCommandHandler<MyCommandHandler>();

            // Wait for exit command (because i'm not using a UI application, only console)
            string command;
            do
            {
                command = Console.ReadLine();
            } while (command != "exit");
        }
    }

    public class MyCommandHandler : ICommandHandler
    {
        [Command("ping")]
        public void OnPingCommand(ICharacter sender)
        {
            sender.ReceiveChatMessage("pong", ChatMessageColor.GREEN);
        }
    }

    public class MySessionListener : IEventListener
    {
        [Handler]
        public void OnTargetMove(TargetMoveEvent e)
        {
            ICharacter character = e.Character;
            ILivingEntity target = character.Target.Entity;

            character.Move(target.Position);
            character.ShowBubbleMessage($"Following {target.EntityType}:{target.Id}");
        }
    }

    public class MyGlobalListener : IEventListener
    {
        [Handler]
        public void OnEntitySpawn(EntityJoinEvent e)
        {
            if (e.Entity.EntityType == EntityType.PLAYER)
            {
                var player = e.Entity.As<IPlayer>();
                
                e.Client.Character.ReceiveChatMessage($"{player.Name} / Lv.{player.Level} joined map {e.Map.Id}", ChatMessageColor.YELLOW);
            }
        }
    }
}