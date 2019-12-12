using NtCore.Enums;

namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("c_info", PacketType.Recv)]
    public class CInfoPacket : Packet
    {
        [PacketIndex(0)]
        public string Name { get; set; }

        [PacketIndex(5)]
        public int CharacterId { get; set; }

        [PacketIndex(7)]
        public Gender Gender { get; set; }

        [PacketIndex(10)]
        public ClassType Class { get; set; }
    }
}