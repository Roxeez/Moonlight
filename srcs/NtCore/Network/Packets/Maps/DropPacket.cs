namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("drop", PacketType.Recv)]
    public class DropPacket : Packet
    {
        [PacketIndex(1)]
        public short VNum { get; set; }

        [PacketIndex(2)]
        public int DropId { get; set; }

        [PacketIndex(3)]
        public short PositionX { get; set; }

        [PacketIndex(4)]
        public short PositionY { get; set; }

        [PacketIndex(5)]
        public short Amount { get; set; }

        [PacketIndex(7)]
        public int OwnerId { get; set; }
    }
}