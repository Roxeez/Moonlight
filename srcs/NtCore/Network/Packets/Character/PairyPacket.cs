﻿using NtCore.API.Enums;
using NtCore.Enums;

namespace NtCore.Network.Packets.Character
{
    [PacketInfo("pairy", PacketType.Recv)]
    public class PairyPacket : Packet
    {
        [PacketIndex(1)]
        public EntityType EntityType { get; set; }
        
        [PacketIndex(2)]
        public int EntityId { get; set; }
        
        [PacketIndex(3)]
        public Element Element { get; set; }
        
        [PacketIndex(4)]
        public int Power { get; set; }
    }
}