using System;
using System.Reflection;

namespace Moonlight.Conversion
{
    public class ConversionException : Exception
    {
        public ConversionException(object value, MemberInfo target) : base($"Failed to convert {value} to {target.Name}")
        {
        }
    }
}