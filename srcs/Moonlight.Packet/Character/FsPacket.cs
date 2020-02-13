using Moonlight.Core.Enums.Game;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("fs")]
    public class FsPacket : Packet
    {
        [PacketIndex(0)]
        public FactionType Faction { get; set; }
    }
}