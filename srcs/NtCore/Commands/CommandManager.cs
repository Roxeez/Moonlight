using System;
using System.Collections.Generic;
using System.Reflection;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Logger;

namespace NtCore.Commands
{
    public class CommandManager : ICommandManager
    {
        private readonly ILogger _logger;
        private readonly IDictionary<string, (ICommandHandler, MethodInfo)> _registeredCommands = new Dictionary<string, (ICommandHandler, MethodInfo)>();

        public CommandManager(ILogger logger) => _logger = logger;

        public void RegisterCommandHandler(ICommandHandler handler)
        {
            foreach (MethodInfo methodInfo in handler.GetType().GetMethods())
            {
                var command = methodInfo.GetCustomAttribute<CommandAttribute>();
                if (command == null)
                {
                    continue;
                }

                ParameterInfo[] parameters = methodInfo.GetParameters();
                if (parameters.Length == 0 || parameters.Length > 2)
                {
                    continue;
                }

                if (parameters.Length > 0 && parameters[0].ParameterType != typeof(ICharacter))
                {
                    continue;
                }

                if (parameters.Length > 1 && parameters[1].ParameterType != typeof(string[]))
                {
                    continue;
                }

                if (_registeredCommands.ContainsKey(command.Name))
                {
                    _logger.Warning($"[{nameof(CommandManager)}] Command {command.Name} already registered");
                    continue;
                }

                _logger.Information($"Command {command.Name} registered");
                _registeredCommands[command.Name] = (handler, methodInfo);
            }
        }

        public void RegisterCommandHandler<T>() where T : ICommandHandler
        {
            var obj = Activator.CreateInstance<T>();
            RegisterCommandHandler(obj);
        }

        public void ExecuteCommand(IClient client, string command, string[] args)
        {
            (ICommandHandler, MethodInfo) handler = _registeredCommands.GetValueOrDefault(command);

            if (handler == default((ICommandHandler, MethodInfo)))
            {
                client.Character.ShowChatMessage($"There is no command : {command}", ChatMessageColor.RED);
                return;
            }

            ICommandHandler commandHandler = handler.Item1;
            MethodInfo method = handler.Item2;

            ParameterInfo[] parameters = method.GetParameters();

            switch (parameters.Length)
            {
                case 1:
                    method.Invoke(commandHandler, new object[] { client.Character });
                    return;
                case 2:
                    method.Invoke(commandHandler, new object[] { client.Character, args });
                    break;
            }
        }
    }
}