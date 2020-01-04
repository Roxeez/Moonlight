using NtCore.Clients;

namespace NtCore.Events.Battle
{
    public class TargetChangeEvent : Event
    {
        public TargetChangeEvent(IClient client) : base(client)
        {
        }
    }
}