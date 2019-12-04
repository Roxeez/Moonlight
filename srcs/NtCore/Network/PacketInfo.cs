using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtCore.Network
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketInfo : Attribute
    {
        public string Header { get; }
        public PacketType Type { get; }

        public PacketInfo(string header, PacketType type)
        {
            Header = header;
            Type = type;
        }
    }

    public enum PacketType
    {
        Send,
        Recv
    }
}
