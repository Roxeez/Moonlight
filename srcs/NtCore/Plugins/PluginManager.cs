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

        private readonly Dictionary<Type, List<(IListener, MethodInfo)>> _eventHandlers =
            new Dictionary<Type, List<(IListener, MethodInfo)>>();

        private readonly ILogger _logger;

        public PluginManager(ILogger logger, IClientManager clientManager)
        {
            _clientManager = clientManager;
            _logger = logger;
        }

        public void Start(Plugin plugin)
        {
            var info = plugin.GetType().GetCustomAttribute<PluginInfo>();
            if (info != null && info.NeedInjection) _clientManager.CreateLocalClient();

            _logger.Information($"Starting {plugin.Name} {plugin.Version}");
            plugin.OnEnable();
        }

        public void RegisterListeners(Plugin plugin, IListener[] listeners)
        {
            foreach (var listener in listeners)
            foreach (var methodInfo in listener.GetType().GetMethods())
            {
                var handler = methodInfo.GetCustomAttribute<Handler>();
                if (handler == null) continue;

                var parameters = methodInfo.GetParameters();
                if (parameters.Length != 1)
                {
                    _logger.Warning($"Wrong parameter length (Method: {methodInfo.Name} / Plugin: {plugin.Name})");
                    continue;
                }

                var type = methodInfo.GetParameters().First().ParameterType;
                if (!typeof(Event).IsAssignableFrom(type))
                {
                    _logger.Warning(
                        $"Parameter type is not an event (Method: {methodInfo.Name} / Plugin: {plugin.Name})");
                    continue;
                }

                var handlers = _eventHandlers.GetValueOrDefault(type);
                if (handlers == null)
                {
                    handlers = new List<(IListener, MethodInfo)>();
                    _eventHandlers[type] = handlers;
                }

                handlers.Add((listener, methodInfo));
            }
        }

        public void CallEvent(Event e)
        {
            var handlers = _eventHandlers.GetValueOrDefault(e.GetType());

            if (handlers == null) return;

            foreach ((var listener, var methodInfo) in handlers) methodInfo.Invoke(listener, new object[] {e});
        }
    }
}