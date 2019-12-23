namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("at", PacketType.Recv)]
    public class AtPacket : Packet
    {
        [PacketIndex(0)]
        public int CharacterId { get; set; }
        
        [PacketIndex(1)]
        public short MapId { get; set; }
        
        [PacketIndex(2)]
        public short PositionX { get; set; }
        
        [PacketIndex(3)]
        public short PositionY { get; set; }
    }
}