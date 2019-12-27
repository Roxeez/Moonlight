using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Character;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class StatPacketHandler : PacketHandler<StatPacket>
    {
        private readonly IEventManager _eventManager;

        public StatPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, StatPacket packet)
        {
            var character = client.Character.As<Character>();

            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.MaxHp = packet.MaxHp;
            character.MaxMp = packet.MaxMp;

            _eventManager.CallEvent(new StatUpdateEvent(client));
        }
    }
}