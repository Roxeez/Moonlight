using NtCore.Clients;
using NtCore.Game.Entities;

namespace NtCore.Events.Map
{
    public class ItemDropEvent : Event
    {
        public ItemDropEvent(IClient client, IDrop drop) : base(client) => Drop = drop;

        public IDrop Drop { get; }
    }
}