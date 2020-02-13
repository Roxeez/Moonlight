using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class BoolConverter : Converter<bool>
    {
        protected override bool ToObject(string value, Type type, IConversionFactory factory) => value == "1";
        protected override string ToString(bool value, Type type, IConversionFactory factory) => value ? "1" : "0";
    }
}