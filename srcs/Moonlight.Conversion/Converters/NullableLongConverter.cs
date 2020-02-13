using System;

namespace Moonlight.Conversion.Converters
{
    public class NullableLongConverter : Converter<long?>
    {
        protected override long? ToObject(string value, Type type, IConversionFactory factory)
        {
            if (value == "-1")
            {
                return null;
            }

            return (long)factory.ToObject(value, typeof(long));
        }

        protected override string ToString(long? value, Type type, IConversionFactory factory) => value == null ? "-1" : factory.ToString(value, typeof(long));
    }
}