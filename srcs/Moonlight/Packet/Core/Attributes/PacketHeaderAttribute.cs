using System;

namespace Moonlight.Packet.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class PacketHeaderAttribute : Attribute
    {
        public PacketHeaderAttribute(string header) => Header = header;
        public string Header { get; }
    }
}