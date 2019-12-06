using NtCore.API;
using NtCore.API.Enums;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("in", PacketType.Recv)]
    public class InPacket : Packet
    {
        [PacketIndex(1)]
        public EntityType EntityType { get; set; }
        
        public string Name { get; set; }
        public int Vnum { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }
        public byte Direction { get; set; }
        public byte HpPercentage { get; set; }
        public byte MpPercentage { get; set; }
        public int Amount { get; set; }

        public override bool Deserialize(string[] packet)
        {
            base.Deserialize(packet);

            switch (EntityType)
            {
                case EntityType.MONSTER:
                case EntityType.NPC:
                    Vnum = int.Parse(packet[2]);
                    Id = int.Parse(packet[3]);
                    Position = new Position(short.Parse(packet[4]), short.Parse(packet[5]));
                    Direction = byte.Parse(packet[6]);
                    HpPercentage = byte.Parse(packet[7]);
                    MpPercentage = byte.Parse(packet[8]);
                    break;
                case EntityType.DROP:
                    Vnum = int.Parse(packet[2]);
                    Id = int.Parse(packet[3]);
                    Position = new Position(short.Parse(packet[4]), short.Parse(packet[5]));
                    Amount = int.Parse(packet[6]);
                    break;
            }

            return true;
        }
    }
}