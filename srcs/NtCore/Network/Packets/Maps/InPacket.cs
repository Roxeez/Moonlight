using System;
using NtCore.Enums;

namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("in", PacketType.Recv)]
    public class InPacket : Packet
    {
        public EntityType EntityType { get; set; }

        public string Name { get; set; }
        public int Vnum { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }
        public byte Direction { get; set; }
        public byte HpPercentage { get; set; }
        public byte MpPercentage { get; set; }
        public int Amount { get; set; }

        public ClassType ClassType { get; set; }
        public Gender Gender { get; set; }
        public byte Level { get; set; }
        public int DropOwnerId { get; set; }

        public override bool Deserialize(string[] packet)
        {
            EntityType = (EntityType)byte.Parse(packet[0]);
            switch (EntityType)
            {
                case EntityType.MONSTER:
                case EntityType.NPC:
                    Vnum = int.Parse(packet[1]);
                    Id = int.Parse(packet[2]);
                    Position = new Position(short.Parse(packet[3]), short.Parse(packet[4]));
                    Direction = byte.Parse(packet[5]);
                    HpPercentage = byte.Parse(packet[6]);
                    MpPercentage = byte.Parse(packet[7]);
                    break;
                case EntityType.DROP:
                    Vnum = int.Parse(packet[1]);
                    Id = int.Parse(packet[2]);
                    Position = new Position(short.Parse(packet[3]), short.Parse(packet[4]));
                    Amount = int.Parse(packet[5]);
                    DropOwnerId = int.Parse(packet[8]);
                    break;
                case EntityType.PLAYER:
                    Name = packet[1];
                    Id = int.Parse(packet[3]);
                    Position = new Position(short.Parse(packet[4]), short.Parse(packet[5]));
                    Direction = byte.Parse(packet[6]);
                    Gender = (Gender)byte.Parse(packet[8]);
                    ClassType = (ClassType)byte.Parse(packet[11]);
                    HpPercentage = byte.Parse(packet[13]);
                    MpPercentage = byte.Parse(packet[14]);
                    Level = byte.Parse(packet[32]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}