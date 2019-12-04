using System;

namespace NtCore.Network
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PacketIndex : Attribute
    {
        public int Value { get; }

        public PacketIndex(int value)
        {
            Value = value;
        }
    }
}