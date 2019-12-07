using System;
using NtCore.API.Commands;

namespace NtCore.Commands
{
    public class CommandManager : ICommandManager
    {
        public void Register(ICommandHandler handler)
        {
            throw new NotImplementedException();
        }

        public void Execute(string command)
        {
        }
    }
}