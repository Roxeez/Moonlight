using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("c_info")]
    internal class CInfoPacket : Packet
    {
        [PacketIndex(0)]
        public string Name { get; set; }

        [PacketIndex(5)]
        public int CharacterId { get; set; }

        [PacketIndex(7)]
        public GenderType Gender { get; set; }

        [PacketIndex(10)]
        public ClassType Class { get; set; }
    }
}