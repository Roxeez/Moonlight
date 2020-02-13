using System;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Core
{
    public class PropertyData
    {
        public Type PropertyType { get; set; }
        public PacketIndexAttribute PacketIndexAttribute { get; set; }
        public Delegate Setter { get; set; }
        public Delegate Getter { get; set; }
    }
}