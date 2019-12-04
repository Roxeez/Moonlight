using NtCore.Core;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers
{
    public class StatPacketHandler : PacketHandler<StatPacket>
    {
        public override void Handle(IClient client, StatPacket packet)
        {
            client.Character.Hp = packet.Hp;
            client.Character.Mp = packet.Mp;
            client.Character.MaxHp = packet.MaxHp;
            client.Character.MaxMp = packet.MaxMp;
        }
    }
}