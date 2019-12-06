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

        public ILogger Logger { get; }
        public IPacketManager PacketManager { get;  }
        public IPluginManager PluginManager { get;  }

        private bool _localAlreadyExist;
        
        public NtCore(IPacketManager packetManager, IPluginManager pluginManager, ILogger logger)
        {
            PacketManager = packetManager;
            PluginManager = pluginManager;
            Logger = logger;
        }

        public void CreateLocalClient()
        {
            if (_localAlreadyExist)
            {
                return;
            }
            
            Process process = Process.GetCurrentProcess();
            
            if (process.MainModule == null)
            {
                throw new InvalidOperationException("Process module can't be null");
            }
            
            LocalClient localClient = new LocalClient(process.MainModule);

            localClient.PacketReceived += packet => PacketManager.Handle(localClient, packet, PacketType.Recv);
            localClient.PacketSend += packet => PacketManager.Handle(localClient, packet, PacketType.Send);

            _localAlreadyExist = true;
        }

        public void Load(IServiceCollection services)
        {
            Logger.Information("Initializing NtCore...");
            
            services.AddSingleton<IMapManager, MapManager>();
        }
    }
}