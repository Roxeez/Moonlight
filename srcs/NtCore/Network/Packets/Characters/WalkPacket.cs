namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("walk", PacketType.Send)]
    public class WalkPacket : Packet
    {
        [PacketIndex(0)]
        public byte X { get; set; }

        [PacketIndex(1)]
        public byte Y { get; set; }

        [PacketIndex(3)]
        public byte Speed { get; set; }
    }
}