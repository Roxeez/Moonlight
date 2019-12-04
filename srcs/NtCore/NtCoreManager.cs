using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API.Client;
using NtCore.API.Managers;
using NtCore.Logging;
using NtCore.Managers;
using NtCore.Network;

namespace NtCore
{
    public class NtCoreManager
    {
        private readonly IPacketManager _packetManager;
        
        public NtCoreManager()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IMapManager, MapManager>();
            services.AddSingleton<ILogger, ConsoleLogger>();

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

            _packetManager = provider.GetService<IPacketManager>();
            _packetManager.Initialize(provider);
        }
        
        public IClient CreateClient()
        {
            Process process = Process.GetCurrentProcess();

            if (process.MainModule == null)
            {
                throw new InvalidOperationException("Process module can't be null");
            }
            
            Client client = new Client(process.MainModule);

            client.PacketReceived += packet => _packetManager.Handle(client, packet, PacketType.Recv);
            client.PacketSend += packet => _packetManager.Handle(client, packet, PacketType.Send);

            return client;
        }
    }
}