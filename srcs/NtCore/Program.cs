using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Core;
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
            Kernel32.AllocConsole();

            Console.Title = "NtCore";

            ShowHeader();
            
            var logger = new ConsoleLogger();
            
            logger.Information("Registering core services...");
            var services = new ServiceCollection();

            BuildCore(services);
            BuildPacketHandlers(services);
            BuildPlugins(services);

            var core = services.BuildServiceProvider();
            logger.Information("Core services registered.");
            
            logger.Information("Initializing NtCoreAPI...");
            NtCoreAPI.Initialize(core.GetService<INtCore>());
            logger.Information("NtCoreAPI initialized.");
            
            IPacketManager packetManager = core.GetService<IPacketManager>();
            IPluginManager pluginManager = core.GetService<IPluginManager>();

            logger.Information("Registering packet handlers...");
            foreach (IPacketHandler packetHandler in core.GetServices<IPacketHandler>())
            {
                packetManager.Register(packetHandler);
            }
            logger.Information("Packet handlers registered.");

            logger.Information("Starting plugins...");
            foreach (Plugin plugin in core.GetServices<Plugin>())
            {
                pluginManager.Start(plugin);
            }
            logger.Information("Plugins started.");

            string command;
            do
            {
                command = Console.ReadLine();
            } 
            while (command != "exit");
        }

        private static void ShowHeader()
        {
            const string text = @"
  _   _ _    _____               
 | \ | | |  / ____|              
 |  \| | |_| |     ___  _ __ ___ 
 | . ` | __| |    / _ \| '__/ _ \
 | |\  | |_| |___| (_) | | |  __/
 |_| \_|\__|\_____\___/|_|  \___|
";
            
            string separator = new string('=', Console.WindowWidth);
            string logo = text.Split('\n').Select(s => string.Format("{0," + (Console.WindowWidth / 2 + s.Length / 2) + "}\n", s))
                .Aggregate("", (current, i) => current + i);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(separator + logo + separator);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static IServiceProvider UnitTestProvider()
        {
            var services = new ServiceCollection();
            
            BuildCore(services);
            BuildPacketHandlers(services);
            
            var core = services.BuildServiceProvider();

            IPacketManager packetManager = core.GetService<IPacketManager>();

            foreach (IPacketHandler packetHandler in core.GetServices<IPacketHandler>())
            {
                packetManager.Register(packetHandler);
            }

            return core;
        }

        private static void BuildCore(IServiceCollection services)
        {
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IScheduler, ObservableScheduler>();

            services.AddSingleton<IClientManager, ClientManager>();
            services.AddSingleton<IPluginManager, PluginManager>();
            services.AddSingleton<IPacketManager, PacketManager>();
            services.AddSingleton<IMapManager, MapManager>();
            
            services.AddSingleton<INtCore, NtCore>();
        }

        private static void BuildPacketHandlers(IServiceCollection services)
        {
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
        }

        private static void BuildPlugins(IServiceCollection services)
        {
            if (!Directory.Exists(PluginManager.PluginDirectory))
            {
                Directory.CreateDirectory(PluginManager.PluginDirectory);
            }

            string[] plugins = Directory.GetFiles(PluginManager.PluginDirectory);
            if (plugins.Length == 0)
            {
                return;
            }
            
            foreach (string file in plugins)
            {
                Assembly assembly = Assembly.LoadFile(Path.Combine(PluginManager.PluginDirectory, file));
                Type pluginMain = assembly.GetTypes().FirstOrDefault(x => typeof(Plugin).IsAssignableFrom(x));

                if (pluginMain == null)
                {
                    continue;
                }

                PluginInfo info = pluginMain.GetCustomAttribute<PluginInfo>();
                if (info == null)
                {
                    continue;
                }
                
                services.AddSingleton(typeof(Plugin), pluginMain);
            }
        }

    }
}