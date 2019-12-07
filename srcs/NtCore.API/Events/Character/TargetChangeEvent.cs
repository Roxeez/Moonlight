using NtCore.API.Clients;
using NtCore.API.Game.Battle;
using NtCore.API.Plugins;

namespace NtCore.API.Events.Character
{
    public class TargetChangeEvent : Event
    {
        public IClient Client { get; }
        public ITarget Target { get; }

        public TargetChangeEvent(IClient client, ITarget target)
        {
            Client = client;
            Target = target;
        }
    }
}