using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class NullableByteConverter : Converter<byte?>
    {
        protected override byte? ToObject(string value, Type type, IConversionFactory factory)
        {
            if (value == "-1" || value == "-")
            {
                return null;
            }

            return (byte)factory.ToObject(value, typeof(byte));
        }

        protected override string ToString(byte? value, Type type, IConversionFactory factory) => value == null ? "-1" : factory.ToString(value, typeof(byte));
    }
}