using System;

namespace NtCore.Network
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Index : Attribute
    {
        public int Value { get; }

        public Index(int value)
        {
            Value = value;
        }
    }
}