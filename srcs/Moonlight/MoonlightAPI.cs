using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Enums.Game;
using Moonlight.Core.Enums.Translation;
using Moonlight.Core.Extensions;
using Moonlight.Core.Import;
using Moonlight.Extensions;
using Moonlight.Core.Logging;
using Moonlight.Database.Extensions;
using Moonlight.Event;
using Moonlight.Handlers;
using Moonlight.Packet.Extensions;
using Moonlight.Translation;

[assembly: InternalsVisibleTo("Moonlight.Tests")]
namespace Moonlight
{
    public sealed class MoonlightAPI
    {
        private readonly IClientManager _clientManager;
        private readonly ILanguageService _languageService;
        private readonly IPacketHandlerManager _packetHandlerManager;
        private readonly IEventManager _eventManager;

        public MoonlightAPI() : this(new AppConfig())
        {
        }

        internal MoonlightAPI(AppConfig config)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogger();
            services.AddPacketDependencies();
            services.AddDatabaseDependencies(config);
            services.AddFactories();

            services.AddSingleton<ILanguageService, LanguageService>();
            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IPacketHandlerManager, PacketHandlerManager>();
            services.AddSingleton<IEventManager, EventManager>();

            services.AddImplementingTypes<IPacketHandler>();

            IServiceProvider provider = services.BuildServiceProvider();

            _clientManager = provider.GetService<IClientManager>();
            _packetHandlerManager = provider.GetService<IPacketHandlerManager>();
            _languageService = provider.GetService<ILanguageService>();
            _eventManager = provider.GetService<IEventManager>();

            Logger = provider.GetService<ILogger>();
        }

        public Language Language
        {
            get => _languageService.Language;
            set => _languageService.Language = value;
        }

        public ILogger Logger { get; }

        public Client CreateLocalClient() => _clientManager.CreateLocalClient();

        public void AddListener<T>(EventListener<T> listener) where T : IEventNotification
        {
            _eventManager.RegisterListener(listener);
        }

        public void CreateConsole()
        {
            Kernel32.AllocConsole();
        }

        /**
         * Trick to use it in tests to handling mock packet send/recv but hide to classic users
         */
        internal IPacketHandlerManager GetPacketHandlerManager() => _packetHandlerManager;
    }
}