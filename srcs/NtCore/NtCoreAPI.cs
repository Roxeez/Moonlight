using System;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API.Scheduler;
using NtCore.Clients;
using NtCore.Commands;
using NtCore.Events;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.Logger;
using NtCore.Network;
using NtCore.Scheduler;

namespace NtCore
{
    public sealed class NtCoreAPI
    {
        public ILogger Logger { get; }
        public IScheduler Scheduler { get; }
        public IEventManager EventManager { get; }
        public IPacketManager PacketManager { get; }
        public ICommandManager CommandManager { get; }
        public IClientManager ClientManager { get; }
        
        public NtCoreAPI()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IScheduler, ObservableScheduler>();

            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IEventManager, EventManager>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<ICommandManager, CommandManager>();
            services.AddSingleton<IMapManager, MapManager>();
            
            foreach (Type type in typeof(IPacketHandler).Assembly.GetTypes())
            {
                if (!typeof(IPacketHandler).IsAssignableFrom(type))
                {
                    continue;
                }

                if (type.IsAbstract || type.IsInterface || !type.IsPublic)
                {
                    continue;
                }

                services.AddSingleton(typeof(IPacketHandler), type);
            }
            
            ServiceProvider core = services.BuildServiceProvider();

            Logger = core.GetService<ILogger>();
            Scheduler = core.GetService<IScheduler>();
            EventManager = core.GetService<IEventManager>();
            CommandManager = core.GetService<ICommandManager>();
            PacketManager = core.GetService<IPacketManager>();
            ClientManager = core.GetService<IClientManager>();
            
            foreach (IPacketHandler packetHandler in core.GetServices<IPacketHandler>())
            {
                PacketManager.Register(packetHandler);
            }
            
            Logger.Information("NtCoreAPI successfully initialized.");
        }
    }
}