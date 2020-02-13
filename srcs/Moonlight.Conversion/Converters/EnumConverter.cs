using System;

namespace Moonlight.Conversion.Converters
{
    public class EnumConverter : Converter<Enum>
    {
        public override bool IsGeneric => true;

        protected override Enum ToObject(string value, Type type, IConversionFactory factory)
        {
            try
            {
                return (Enum)Enum.Parse(type, value);
            }
            catch (Exception)
            {
                throw new ConversionException(value, type);
            }
        }

        protected override string ToString(Enum value, Type type, IConversionFactory factory) => factory.ToString(value, typeof(int));
    }
}