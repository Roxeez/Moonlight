using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NtCore.API;
using NtCore.API.Core;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Plugins;
using EventHandler = NtCore.API.Plugins.EventHandler;

namespace NtCore.Core
{
    public class PluginManager : IPluginManager
    {
        private readonly Dictionary<Type, List<(IListener, MethodInfo)>> _eventHandlers = new Dictionary<Type, List<(IListener, MethodInfo)>>();
        
        private readonly ILogger _logger;
        private readonly IClientManager _clientManager;
        
        public PluginManager(ILogger logger, IClientManager clientManager)
        {
            _clientManager = clientManager;
            _logger = logger;
        }

        public void Load(IServiceCollection services)
        {
            if (!Directory.Exists(NtCore.PluginsFolder))
            {
                Directory.CreateDirectory(NtCore.PluginsFolder);
            }

            string[] plugins = Directory.GetFiles(NtCore.PluginsFolder);

            if (plugins.Length == 0)
            {
                _logger.Warning("Plugin folder is empty");
                return;
            }

            foreach (string file in plugins)
            {
                Assembly assembly = Assembly.LoadFile(Path.Combine(NtCore.PluginsFolder, file));
                Type pluginMain = assembly.GetTypes().FirstOrDefault(x => typeof(Plugin).IsAssignableFrom(x));

                if (pluginMain == null)
                {
                    _logger.Warning($"Can't load plugin {file} unable to find Plugin main class");
                    continue;
                }

                PluginInfo info = pluginMain.GetCustomAttribute<PluginInfo>();
                if (info == null)
                {
                    _logger.Warning($"Can't load plugin {file}, main class without PluginInfo attribute");
                    continue;
                }
                
                services.AddSingleton(typeof(Plugin), pluginMain);
            }
        }

        public void Start(IServiceProvider serviceProvider)
        {
            IEnumerable<Plugin> plugins = serviceProvider.GetServices<Plugin>();

            foreach (Plugin plugin in plugins)
            {
                plugin.Run();
            }

            if (plugins.Any(x => x.GetType().GetCustomAttribute<PluginInfo>().IsInjected))
            {
                _clientManager.CreateLocalClient();
            }
        }
        
        public void Register(IListener[] listeners)
        {
            foreach (IListener listener in listeners)
            {
                foreach (MethodInfo methodInfo in listener.GetType().GetMethods())
                {
                    EventHandler eventHandler = methodInfo.GetCustomAttribute<EventHandler>();
                    if (eventHandler == null)
                    {
                        continue;
                    }

                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    if (parameters.Length != 1)
                    {
                        _logger.Warning("Wrong parameter length");
                        continue;
                    }

                    Type type = methodInfo.GetParameters().First().ParameterType;
                    if (!typeof(Event).IsAssignableFrom(type))
                    {
                        _logger.Warning("Parameter type is not an event");
                        continue;
                    }

                    List<(IListener, MethodInfo)> handlers = _eventHandlers.GetValueOrDefault(type);
                    if (handlers == null)
                    {
                        handlers = new List<(IListener, MethodInfo)>();
                        _eventHandlers[type] = handlers;
                    }
                    
                    _logger.Information($"Registered handler for event {type.Name}");
                    handlers.Add((listener, methodInfo));
                }
            }
        }

        public void CallEvent(Event e)
        {
            List<(IListener, MethodInfo)> handlers = _eventHandlers.GetValueOrDefault(e.GetType());

            if (handlers == null)
            {
                return;
            }
            
            foreach ((IListener listener, MethodInfo methodInfo) in handlers)
            {
                methodInfo.Invoke(listener, new object[] { e });
            }
        }
    }
}