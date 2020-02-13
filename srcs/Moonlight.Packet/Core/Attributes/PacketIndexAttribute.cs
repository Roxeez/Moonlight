using System;

namespace Moonlight.Packet.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PacketIndexAttribute : Attribute
    {
        public PacketIndexAttribute(int index) => Index = index;

        public int Index { get; }
        public string Separator { get; set; } = " ";
    }
}