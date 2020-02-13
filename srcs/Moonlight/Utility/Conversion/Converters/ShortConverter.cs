using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class ShortConverter : Converter<short>
    {
        protected override short ToObject(string value, Type type, IConversionFactory factory)
        {
            if (!short.TryParse(value, out short parsed))
            {
                throw new ConversionException(value, type);
            }

            return parsed;
        }

        protected override string ToString(short value, Type type, IConversionFactory factory) => value.ToString();
    }
}