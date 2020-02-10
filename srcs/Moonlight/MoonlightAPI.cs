using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Extensions;
using Moonlight.Core.Logging;
using Moonlight.Database.Extensions;
using Moonlight.Game.Extensions;
using Moonlight.Game.Handlers;
using Moonlight.Packet.Extensions;
using Moonlight.Translation;

[assembly: InternalsVisibleTo("Moonlight.Tests")]
[assembly: InternalsVisibleTo("Moonlight.Toolkit")]

namespace Moonlight
{
    public sealed class MoonlightAPI
    {
        private readonly IClientManager _clientManager;
        private readonly ILanguageService _languageService;
        private readonly IPacketHandlerManager _packetHandlerManager;

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

            services.AddImplementingTypes<IPacketHandler>();

            IServiceProvider provider = services.BuildServiceProvider();
            
            _clientManager = provider.GetService<IClientManager>();
            _packetHandlerManager = provider.GetService<IPacketHandlerManager>();
            _languageService = provider.GetService<ILanguageService>();

            Logger = provider.GetService<ILogger>();
        }

        public Language Language
        {
            get => _languageService.Language;
            set => _languageService.Language = value;
        }

        public ILogger Logger { get; }

        public Client CreateLocalClient() => _clientManager.CreateLocalClient();

        /**
         * Trick to use it in tests to handling mock packet send/recv but hide to classic users
         */
        internal IPacketHandlerManager GetPacketHandlerManager() => _packetHandlerManager;
    }
}