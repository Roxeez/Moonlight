using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("fd")]
    internal class FdPacket : Packet
    {
        [PacketIndex(0)]
        public int Reputation { get; set; }

        [PacketIndex(2)]
        public int Dignity { get; set; }
    }
}