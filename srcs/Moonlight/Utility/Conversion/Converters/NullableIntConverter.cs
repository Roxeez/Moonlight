using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class NullableIntConverter : Converter<int?>
    {
        protected override int? ToObject(string value, Type type, IConversionFactory factory)
        {
            if (value == "-1")
            {
                return null;
            }

            return (int)factory.ToObject(value, typeof(int));
        }

        protected override string ToString(int? value, Type type, IConversionFactory factory) => value == null ? "-1" : factory.ToString(value, typeof(int));
    }
}