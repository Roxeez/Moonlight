using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers.Character
{
    public class StatPacketHandler : PacketHandler<StatPacket>
    {
        public override void Handle(IClient client, StatPacket packet)
        {
            var character = client.Character.AsModifiable();
            
            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.MaxHp = packet.MaxHp;
            character.MaxMp = packet.MaxMp;
        }
    }
}