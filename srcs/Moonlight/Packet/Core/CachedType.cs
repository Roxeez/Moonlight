using System;
using System.Collections.Generic;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Core
{
    internal class CachedType
    {
        public PacketHeaderAttribute PacketHeaderAttribute { get; set; }
        public Type PacketType { get; set; }
        public Delegate Constructor { get; set; }
        public List<PropertyData> Properties { get; set; }
    }
}