using NtCore.API.Enums;

namespace NtCore.Network.Packets.Character
{
    [PacketInfo("fs", PacketType.Recv)]
    public class FactionPacket : Packet
    {
        public Faction Faction { get; set; }

        public override bool Deserialize(string[] packet)
        {
            byte factionId = byte.Parse(packet[1]);
            Faction = (Faction) factionId;

            return true;
        }
    }
}