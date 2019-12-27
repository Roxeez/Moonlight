using System;
using Microsoft.Extensions.DependencyInjection;
using NtCore.Clients;
using NtCore.Commands;
using NtCore.Events;
using NtCore.Factory;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.I18N;
using NtCore.Logger;
using NtCore.Network;
using NtCore.Registry;
using NtCore.Resources;
using NtCore.Scheduler;
using NtCore.Serialization;
using NtCore.Services.Gameforge;

namespace NtCore
{
    public static class NtCoreAPI
    {
        private static readonly IClientManager ClientManager;
        private static readonly ICommandManager CommandManager;
        private static readonly IEventManager EventManager;
        private static readonly ILogger Logger;
        private static readonly IPacketManager PacketManager;
        private static readonly IScheduler Scheduler;
        private static readonly IRegistry Registry;
        private static readonly ILanguageService LanguageService;

        static NtCoreAPI()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IScheduler, ObservableScheduler>();

            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IEventManager, EventManager>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<ICommandManager, CommandManager>();
            services.AddSingleton<IEntityFactory, EntityFactory>();
            services.AddSingleton<ISkillFactory, SkillFactory>();
            services.AddSingleton<IMapManager, MapManager>();
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddSingleton<IGameforgeAuthService, GameforgeAuthService>();
            services.AddSingleton<ResourceManager>();

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

            services.AddSingleton<IRegistry, GameRegistry>();
            services.AddSingleton<ILanguageService, LanguageService>();

            ServiceProvider core = services.BuildServiceProvider();

            Registry = core.GetService<IRegistry>();
            Logger = core.GetService<ILogger>();
            Scheduler = core.GetService<IScheduler>();
            EventManager = core.GetService<IEventManager>();
            CommandManager = core.GetService<ICommandManager>();
            PacketManager = core.GetService<IPacketManager>();
            ClientManager = core.GetService<IClientManager>();
            LanguageService = core.GetService<ILanguageService>();

            Registry.Load();
            LanguageService.Load("uk");

            foreach (IPacketHandler packetHandler in core.GetServices<IPacketHandler>())
            {
                PacketManager.Register(packetHandler);
            }

            Logger.Information("NtCoreAPI successfully initialized.");
        }

        public static IClientManager GetClientManager() => ClientManager;
        public static IPacketManager GetPacketManager() => PacketManager;
        public static ILogger GetLogger() => Logger;
        public static IEventManager GetEventManager() => EventManager;
        public static ICommandManager GetCommandManager() => CommandManager;
        public static IScheduler GetScheduler() => Scheduler;
        public static IRegistry GetRegistry() => Registry;
        public static ILanguageService GetLanguageService() => LanguageService;
    }
}