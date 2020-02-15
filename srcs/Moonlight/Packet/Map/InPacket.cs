using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map
{
    [PacketHeader("in")]
    internal class InPacket : Packet
    {
        [PacketIndex(0)]
        public EntityType EntityType { get; set; }

        [PacketIndex(1)]
        public string Name { get; set; }

        [PacketIndex(2)]
        public int Vnum { get; set; }

        [PacketIndex(3)]
        public int EntityId { get; set; }

        [PacketIndex(4)]
        public short PositionX { get; set; }

        [PacketIndex(5)]
        public short PositionY { get; set; }

        [PacketIndex(6)]
        public byte Direction { get; set; }

        public InPlayerSubPacket PlayerSubPacket { get; set; }
        public InNpcSubPacket NpcSubPacket { get; set; }
        public InDropSubPacket DropSubPacket { get; set; }
    }

    internal class InNpcSubPacket : Packet
    {
        [PacketIndex(0)]
        public byte HpPercentage { get; set; }

        [PacketIndex(1)]
        public byte MpPercentage { get; set; }

        [PacketIndex(3)]
        public FactionType Faction { get; set; }

        [PacketIndex(5)]
        public long? Owner { get; set; }

        [PacketIndex(9)]
        public string Name { get; set; }
    }

    internal class InPlayerSubPacket : Packet
    {
        [PacketIndex(1)]
        public GenderType Gender { get; set; }

        [PacketIndex(4)]
        public ClassType Class { get; set; }

        [PacketIndex(6)]
        public byte HpPercentage { get; set; }

        [PacketIndex(7)]
        public byte MpPercentage { get; set; }
    }

    internal class InDropSubPacket : Packet
    {
        [PacketIndex(0)]
        public int Amount { get; set; }

        [PacketIndex(1)]
        public bool IsQuestRelative { get; set; }

        [PacketIndex(2)]
        public long Owner { get; set; }
    }
}