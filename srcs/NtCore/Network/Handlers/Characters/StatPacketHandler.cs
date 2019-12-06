using NtCore.API.Client;
using NtCore.API.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class StatPacketHandler : PacketHandler<StatPacket>
    {
        public override void Handle(IClient client, StatPacket packet)
        {
            var character = client.Character.As<Character>();
            
            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.MaxHp = packet.MaxHp;
            character.MaxMp = packet.MaxMp;
        }
    }
}