using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Logger;

namespace NtCore.Events
{
    public class EventManager : IEventManager
    {
        private readonly IDictionary<Type, List<HandlerInfo>> _eventHandlers = new Dictionary<Type, List<HandlerInfo>>();
        private readonly ILogger _logger;

        public EventManager(ILogger logger) => _logger = logger;

        public void RegisterEventListener(IEventListener eventListener, IClient client)
        {
            foreach (MethodInfo methodInfo in eventListener.GetType().GetMethods())
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

                List<HandlerInfo> handlers = _eventHandlers.GetValueOrDefault(type);
                if (handlers == null)
                {
                    handlers = new List<HandlerInfo>();
                    _eventHandlers[type] = handlers;
                }

                _logger.Information($"{type.Name} handler registered");
                handlers.Add(new HandlerInfo(eventListener, methodInfo, client));
            }
        }

        public void RegisterEventListener(IEventListener eventListener)
        {
            RegisterEventListener(eventListener, null);
        }

        public void RegisterEventListener<T>(IClient client) where T : IEventListener
        {
            var obj = Activator.CreateInstance<T>();
            RegisterEventListener(obj, client);
        }

        public void RegisterEventListener<T>() where T : IEventListener
        {
            RegisterEventListener<T>(null);
        }

        public void CallEvent(Event e)
        {
            List<HandlerInfo> handlers = _eventHandlers.GetValueOrDefault(e.GetType());

            if (handlers == null)
            {
                return;
            }

            foreach (HandlerInfo handler in handlers)
            {
                if (handler.BindClient != null && !handler.BindClient.Equals(e.Client))
                {
                    continue;
                }
                handler.Method.Invoke(handler.Listener, new object[] { e });
            }
        }
    }

    public class HandlerInfo
    {
        public IEventListener Listener { get; }
        public MethodInfo Method { get; }
        public IClient BindClient { get; }

        public HandlerInfo(IEventListener listener, MethodInfo method, IClient bindClient = null)
        {
            Listener = listener;
            Method = method;
            BindClient = bindClient;
        }
    }
}