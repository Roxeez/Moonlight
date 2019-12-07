using NtCore.API.Clients;
using NtCore.API.Plugins;

namespace NtCore.API.Commands
{
    public interface ICommandManager
    {
        void Register(ICommandHandler handler, Plugin plugin);
        void Register<T>(Plugin plugin) where T : ICommandHandler;
        void Execute(IClient client, string command, string[] args);
    }
}