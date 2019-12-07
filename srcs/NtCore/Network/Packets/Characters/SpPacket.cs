namespace NtCore.Network.Packets.Characters
{
    [PacketInfo("sp", PacketType.Recv)]
    public class SpPacket : Packet
    {
        [PacketIndex(1)] public int AdditionalPoints { get; set; }

        [PacketIndex(2)] public int MaximumAdditionalPoints { get; set; }

        [PacketIndex(3)] public int Points { get; set; }

        [PacketIndex(4)] public int MaximumPoints { get; set; }
    }
}