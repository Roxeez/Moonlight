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

namespace NtCore.Core
{
    public class PluginManager : IPluginManager
    {
        private static readonly string PluginDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NtCore"), "plugins");
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
            if (!Directory.Exists(PluginDirectory))
            {
                _logger.Information($"Creating {PluginDirectory} folder");
                Directory.CreateDirectory(PluginDirectory);
            }

            string[] plugins = Directory.GetFiles(PluginDirectory);
            if (plugins.Length == 0)
            {
                _logger.Warning("Plugin folder is empty");
                return;
            }
            
            _logger.Information($"Found {plugins}");
            foreach (string file in plugins)
            {
                Assembly assembly = Assembly.LoadFile(Path.Combine(PluginDirectory, file));
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

        public void Start(IEnumerable<Plugin> plugins)
        {
            foreach (Plugin plugin in plugins)
            {
                _logger.Information($"Starting {plugin.Name} {plugin.Version}");
                plugin.OnEnable();
            }

            if (plugins.Any(x => x.GetType().GetCustomAttribute<PluginInfo>().IsInjected))
            {
                _logger.Information("Local client created successfully");
                _clientManager.CreateLocalClient();
            }
        }

        public void RegisterListeners(Plugin plugin, IListener[] listeners)
        {
            foreach (IListener listener in listeners)
            {
                foreach (MethodInfo methodInfo in listener.GetType().GetMethods())
                {
                    Handler handler = methodInfo.GetCustomAttribute<Handler>();
                    if (handler == null)
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