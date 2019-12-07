using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.API.Logger;
using NtCore.API.Plugins;

namespace NtCore.Plugins
{
    public class PluginManager : IPluginManager
    {
        public static readonly string PluginDirectory =
            Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NtCore"),
                "plugins");

        private readonly IClientManager _clientManager;

        private readonly Dictionary<Type, List<(IListener, MethodInfo)>> _eventHandlers = new Dictionary<Type, List<(IListener, MethodInfo)>>();

        private readonly ILogger _logger;

        public PluginManager(ILogger logger, IClientManager clientManager)
        {
            _clientManager = clientManager;
            _logger = logger;
        }

        public void Start(Plugin plugin)
        {
            if (_clientManager.LocalClient == null)
            {
                _clientManager.CreateLocalClient();
            }

            _logger.Information($"Starting {plugin.Name} {plugin.Version}");
            plugin.OnEnable();
        }

        public void Register(IListener listener, Plugin plugin)
        {
            foreach (MethodInfo methodInfo in listener.GetType().GetMethods())
            {
                var handler = methodInfo.GetCustomAttribute<Handler>();
                if (handler == null)
                {
                    continue;
                }

                ParameterInfo[] parameters = methodInfo.GetParameters();
                if (parameters.Length != 1)
                {
                    continue;
                }

                Type type = methodInfo.GetParameters().First().ParameterType;
                if (!typeof(Event).IsAssignableFrom(type))
                {
                    continue;
                }

                List<(IListener, MethodInfo)> handlers = _eventHandlers.GetValueOrDefault(type);
                if (handlers == null)
                {
                    handlers = new List<(IListener, MethodInfo)>();
                    _eventHandlers[type] = handlers;
                }

                plugin.Logger.Information($"{type.Name} handler registered");
                handlers.Add((listener, methodInfo));
            }
        }

        public void Register<T>(Plugin plugin) where T : IListener
        {
            var obj = Activator.CreateInstance<T>();
            Register(obj, plugin);
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