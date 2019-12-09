using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NtCore.Extensions;
using NtCore.Logger;

namespace NtCore.Events
{
    public class EventManager : IEventManager
    {
        private readonly IDictionary<Type, List<(IEventListener, MethodInfo)>> _eventHandlers = new Dictionary<Type, List<(IEventListener, MethodInfo)>>();
        private readonly ILogger _logger;

        public EventManager(ILogger logger) => _logger = logger;

        public void RegisterEventListener(IEventListener eventListener)
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

                List<(IEventListener, MethodInfo)> handlers = _eventHandlers.GetValueOrDefault(type);
                if (handlers == null)
                {
                    handlers = new List<(IEventListener, MethodInfo)>();
                    _eventHandlers[type] = handlers;
                }

                _logger.Information($"{type.Name} handler registered");
                handlers.Add((eventListener, methodInfo));
            }
        }

        public void RegisterEventListener<T>() where T : IEventListener
        {
            var obj = Activator.CreateInstance<T>();
            RegisterEventListener(obj);
        }

        public void CallEvent(Event e)
        {
            List<(IEventListener, MethodInfo)> handlers = _eventHandlers.GetValueOrDefault(e.GetType());

            if (handlers == null)
            {
                return;
            }

            foreach ((IEventListener listener, MethodInfo methodInfo) in handlers)
            {
                methodInfo.Invoke(listener, new object[] { e });
            }
        }
    }
}