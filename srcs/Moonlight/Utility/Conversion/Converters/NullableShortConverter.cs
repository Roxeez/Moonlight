using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class NullableShortConverter : Converter<short?>
    {
        protected override short? ToObject(string value, Type type, IConversionFactory factory)
        {
            if (value == "-1")
            {
                return null;
            }

            return (short)factory.ToObject(value, typeof(short));
        }

        protected override string ToString(short? value, Type type, IConversionFactory factory) => value == null ? "-1" : factory.ToString(value, typeof(short));
    }
}