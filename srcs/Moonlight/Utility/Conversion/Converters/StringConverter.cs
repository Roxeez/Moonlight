using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class StringConverter : Converter<string>
    {
        protected override string ToObject(string value, Type type, IConversionFactory factory) => value;
        protected override string ToString(string value, Type type, IConversionFactory factory) => value;
    }
}