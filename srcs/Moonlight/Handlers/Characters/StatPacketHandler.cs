using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Packet.Character;

namespace Moonlight.Handlers.Characters
{
    internal class StatPacketHandler : PacketHandler<StatPacket>
    {
        protected override void Handle(Client client, StatPacket packet)
        {
            Character character = client.Character;

            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.MaxHp = packet.MaxHp;
            character.MaxMp = packet.MaxMp;
        }
    }
}