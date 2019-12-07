using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Core;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Managers;
using NtCore.API.Plugins;
using NtCore.API.Scheduler;
using NtCore.Core;
using NtCore.Import;
using NtCore.Managers;
using NtCore.Network;
using NtCore.Scheduler;

namespace NtCore
{
    public static class Program
    {
        [DllExport]
        public static void Main()
        {
            var t = new Thread(() =>
            {
                Kernel32.AllocConsole();
                
                Build();

                Console.ReadKey();
            });
            
            t.Start();
        }
        
        public static IServiceProvider Build()
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<ILogger>(new ConsoleLogger("NtCore"));
            services.AddSingleton<IScheduler, ObservableScheduler>();
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IMapManager, MapManager>();

            var root = services.BuildServiceProvider();
            
            NtCoreAPI.Initialize(root.GetService<IScheduler>(), root.GetService<IPluginManager>(), root.GetService<ILogger>());
            
            root.GetService<IPacketManager>().As<PacketManager>().Load(services);
            root.GetService<IPluginManager>().As<PluginManager>().Load(services);

            root = services.BuildServiceProvider();
            
            root.GetService<IPacketManager>().As<PacketManager>().Start(root.GetServices<IPacketHandler>());
            root.GetService<IPluginManager>().As<PluginManager>().Start(root.GetServices<Plugin>());

            return root;
        }
    }
}