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
            EntityType = (EntityType)byte.Parse(packet[1]);
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
                    DropOwnerId = int.Parse(packet[9]);
                    break;
                case EntityType.PLAYER:
                    Name = packet[2];
                    Id = int.Parse(packet[4]);
                    Position = new Position(short.Parse(packet[5]), short.Parse(packet[6]));
                    Direction = byte.Parse(packet[7]);
                    Gender = (Gender)byte.Parse(packet[9]);
                    ClassType = (ClassType)byte.Parse(packet[12]);
                    HpPercentage = byte.Parse(packet[14]);
                    MpPercentage = byte.Parse(packet[15]);
                    Level = byte.Parse(packet[33]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }
}