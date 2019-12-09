using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("c_info", PacketType.Recv)]
    public class CInfoPacket : Packet
    {
        [PacketIndex(1)]
        public string Name { get; set; }

        [PacketIndex(6)]
        public int CharacterId { get; set; }

        [PacketIndex(8)]
        public Gender Gender { get; set; }

        [PacketIndex(11)]
        public ClassType Class { get; set; }
    }
}