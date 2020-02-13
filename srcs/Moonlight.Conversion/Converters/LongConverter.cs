using System;

namespace Moonlight.Conversion.Converters
{
    public class LongConverter : Converter<long>
    {
        protected override long ToObject(string value, Type type, IConversionFactory factory)
        {
            if (!long.TryParse(value, out long parsed))
            {
                throw new ConversionException(value, type);
            }

            return parsed;
        }

        protected override string ToString(long value, Type type, IConversionFactory factory) => value.ToString();
    }
}