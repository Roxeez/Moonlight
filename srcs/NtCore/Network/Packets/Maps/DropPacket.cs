namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("drop", PacketType.Recv)]
    public class DropPacket : Packet
    {
        [PacketIndex(0)]
        public short VNum { get; set; }

        [PacketIndex(1)]
        public int DropId { get; set; }

        [PacketIndex(2)]
        public short PositionX { get; set; }

        [PacketIndex(3)]
        public short PositionY { get; set; }

        [PacketIndex(4)]
        public short Amount { get; set; }

        [PacketIndex(6)]
        public int OwnerId { get; set; }
    }
}