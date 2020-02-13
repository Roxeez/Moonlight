using System;
using System.Collections.Generic;
using System.Linq;
using Moonlight.Extensions;
using Moonlight.Toolkit.Commands;
using Moonlight.Toolkit.Handlers;

namespace Moonlight.Toolkit
{
    public class Manager
    {
        private readonly Dictionary<Type, ICommandHandler> _handlers;

        public Manager(IEnumerable<ICommandHandler> handlers)
        {
            _handlers = new Dictionary<Type, ICommandHandler>();
            foreach (ICommandHandler handler in handlers)
            {
                Type commandType = handler.GetType().BaseType?.GenericTypeArguments[0];
                if (commandType == null)
                {
                    continue;
                }

                _handlers[commandType] = handler;
            }
        }

        public Type[] GetCommandTypes() => _handlers.Keys.ToArray();

        public bool Handle(ICommand command)
        {
            if (command == null)
            {
                return false;
            }

            ICommandHandler handler = _handlers.GetValueOrDefault(command.GetType());
            if (handler == null)
            {
                return false;
            }

            return handler.Handle(command);
        }
    }
}