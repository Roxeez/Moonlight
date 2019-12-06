using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Core;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Scheduler;
using NtCore.Core;
using NtCore.Import;
using NtCore.Logger;
using NtCore.Scheduler;

namespace NtCore
{
    public static class Program
    {
        public static NtCore Setup()
        {
            var services = new ServiceCollection();

            services.AddSingleton<NtCore>();
            
            services.AddSingleton<ILogger>(new ConsoleLogger("NtCore"));
            
            services.AddSingleton<IScheduler, ObservableScheduler>();
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IClientManager, ClientManager>();

            var root = services.BuildServiceProvider();

            root.GetService<NtCore>().Load(services);
            
            root.GetService<IPacketManager>().As<PacketManager>().Load(services);
            root.GetService<IPluginManager>().As<PluginManager>().Load(services);

            root = services.BuildServiceProvider();
            
            root.GetService<IPacketManager>().As<PacketManager>().Start(root);
            root.GetService<IPluginManager>().As<PluginManager>().Start(root);

            return root.GetService<NtCore>();
        }
        
        [DllExport]
        public static void Main()
        {
            var t = new Thread(() =>
            {
                Kernel32.AllocConsole();
                Setup();
                
                Console.ReadKey();
            });
            
            t.Start();
        }
    }
}