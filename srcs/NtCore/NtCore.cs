using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Logger;
using NtCore.API.Managers;
using NtCore.Logger;
using NtCore.Managers;
using NtCore.Network;

namespace NtCore
{
    public sealed class NtCore : INtCore
    {
        public IPacketManager PacketManager { get; }
        public IPluginManager PluginManager { get; }
        
        public NtCore()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IMapManager, MapManager>();
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<PluginManager>();

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

            ServiceProvider provider = services.BuildServiceProvider();

            PacketManager = provider.GetService<IPacketManager>();
            PluginManager = provider.GetService<PluginManager>();
            
            PacketManager.Initialize(provider);
        }

        public IClient CreateLocalClient()
        {
            Process process = Process.GetCurrentProcess();
            
            if (process.MainModule == null)
            {
                throw new InvalidOperationException("Process module can't be null");
            }
            
            LocalClient localClient = new LocalClient(process.MainModule);

            localClient.PacketReceived += packet => PacketManager.Handle(localClient, packet, PacketType.Recv);
            localClient.PacketSend += packet => PacketManager.Handle(localClient, packet, PacketType.Send);

            return localClient;
        }
        
        public IClient CreateRemoteClient()
        {
            RemoteClient remoteClient = new RemoteClient();
            
            remoteClient.PacketReceived += packet => PacketManager.Handle(remoteClient, packet, PacketType.Recv);
            remoteClient.PacketSend += packet => PacketManager.Handle(remoteClient, packet, PacketType.Send);

            return remoteClient;
        }
    }
}