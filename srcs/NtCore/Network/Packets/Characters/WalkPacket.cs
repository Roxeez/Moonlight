namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("walk", PacketType.Send)]
    public class WalkPacket : Packet
    {
        [PacketIndex(1)]
        public byte X { get; set; }

        [PacketIndex(2)]
        public byte Y { get; set; }

        [PacketIndex(4)]
        public byte Speed { get; set; }
    }
}