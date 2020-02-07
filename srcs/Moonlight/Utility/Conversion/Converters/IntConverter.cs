using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class IntConverter : Converter<int>
    {
        protected override int ToObject(string value, Type type, IConversionFactory factory)
        {
            if (!int.TryParse(value, out int parsed))
            {
                throw new ConversionException(value, type);
            }

            return parsed;
        }

        protected override string ToString(int value, Type type, IConversionFactory factory) => value.ToString();
    }
}