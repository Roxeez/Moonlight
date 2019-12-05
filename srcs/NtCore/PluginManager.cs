using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NtCore.API;
using NtCore.API.Extensions;
using NtCore.API.Plugins;
using EventHandler = NtCore.API.Plugins.EventHandler;

namespace NtCore
{
    public class PluginManager : IPluginManager
    {
        private readonly Dictionary<Type, List<(IListener, MethodInfo)>> _eventHandlers = new Dictionary<Type, List<(IListener, MethodInfo)>>();

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
                    if (parameters.Length > 1 || parameters.Length < 1)
                    {
                        continue;
                    }

                    Type type = methodInfo.GetParameters().FirstOrDefault()?.ParameterType;
                    if (type == null)
                    {
                        continue;
                    }

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