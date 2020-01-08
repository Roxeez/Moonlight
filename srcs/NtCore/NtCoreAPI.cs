using System;
using Microsoft.Extensions.DependencyInjection;
using NtCore.Clients;
using NtCore.Commands;
using NtCore.Events;
using NtCore.Game.Factory;
using NtCore.Game.Maps;
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
            services.AddSingleton<IItemFactory, ItemFactory>();
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

        /// <summary>
        /// Get client manager
        /// </summary>
        /// <returns>Client manager</returns>
        public static IClientManager GetClientManager() => ClientManager;

        /// <summary>
        /// Get packet manager
        /// </summary>
        /// <returns>Packet manager</returns>
        public static IPacketManager GetPacketManager() => PacketManager;

        /// <summary>
        /// Get current logger
        /// </summary>
        /// <returns>Logger</returns>
        public static ILogger GetLogger() => Logger;

        /// <summary>
        /// Get EventManager
        /// </summary>
        /// <returns>Event manager</returns>
        public static IEventManager GetEventManager() => EventManager;

        /// <summary>
        /// Get CommandManager
        /// </summary>
        /// <returns>Command manager</returns>
        public static ICommandManager GetCommandManager() => CommandManager;

        /// <summary>
        /// Get scheduler
        /// </summary>
        /// <returns>Scheduler</returns>
        public static IScheduler GetScheduler() => Scheduler;

        /// <summary>
        /// Get registry
        /// </summary>
        /// <returns>Registry</returns>
        public static IRegistry GetRegistry() => Registry;

        /// <summary>
        /// Get language service
        /// </summary>
        /// <returns>Language service</returns>
        public static ILanguageService GetLanguageService() => LanguageService;
    }
}