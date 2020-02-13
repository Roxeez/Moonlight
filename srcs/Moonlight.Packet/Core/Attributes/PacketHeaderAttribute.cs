using System;

namespace Moonlight.Packet.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketHeaderAttribute : Attribute
    {
        public PacketHeaderAttribute(string header) => Header = header;
        public string Header { get; }
    }
}