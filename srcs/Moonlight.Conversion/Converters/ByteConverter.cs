using System;

namespace Moonlight.Conversion.Converters
{
    public class ByteConverter : Converter<byte>
    {
        protected override byte ToObject(string value, Type type, IConversionFactory factory)
        {
            if (!byte.TryParse(value, out byte parsed))
            {
                throw new ConversionException(value, type);
            }

            return parsed;
        }

        protected override string ToString(byte value, Type type, IConversionFactory factory) => value.ToString();
    }
}