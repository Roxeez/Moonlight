using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Logger;
using NtCore.API.Managers;
using NtCore.Clients;
using NtCore.Core;
using NtCore.Managers;
using NtCore.Network;

namespace NtCore
{
    public sealed class NtCore
    {
        public static readonly string PluginsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Path.Combine("NtCore", "plugins"));

        private readonly ILogger _logger;
        
        public IPacketManager PacketManager { get;  }
        
        public NtCore(IPacketManager packetManager, ILogger logger)
        {
            PacketManager = packetManager;
            _logger = logger;
        }

        public void Load(IServiceCollection services)
        {
            _logger.Information("Initializing NtCore...");
            
            services.AddSingleton<IMapManager, MapManager>();
        }
    }
}