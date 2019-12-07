using System;

namespace NtCore.Network
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketInfo : Attribute
    {
        public PacketInfo(string header, PacketType type)
        {
            Header = header;
            Type = type;
        }

        public string Header { get; }
        public PacketType Type { get; }
    }

    public enum PacketType
    {
        Send,
        Recv
    }
}