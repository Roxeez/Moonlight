using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("fs")]
    internal class FsPacket : Packet
    {
        [PacketIndex(0)]
        public FactionType Faction { get; set; }
    }
}