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
        private readonly IDictionary<Type, List<(IListener, MethodInfo)>> _eventHandlers = new Dictionary<Type, List<(IListener, MethodInfo)>>();
        private readonly ILogger _logger;
        
        public EventManager(ILogger logger)
        {
            _logger = logger;
        }
        
        public void Register(IListener listener)
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

                _logger.Information($"{type.Name} handler registered");
                handlers.Add((listener, methodInfo));
            }
        }

        public void Register<T>() where T : IListener
        {
            var obj = Activator.CreateInstance<T>();
            Register(obj);
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