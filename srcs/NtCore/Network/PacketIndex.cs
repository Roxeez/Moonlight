using System;

namespace NtCore.Network
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PacketIndex : Attribute
    {
        public PacketIndex(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}