using System;

namespace Moonlight.Utility.Conversion.Converters
{
    public class GuidConverter : Converter<Guid>
    {
        protected override Guid ToObject(string value, Type type, IConversionFactory factory)
        {
            if (!Guid.TryParse(value, out Guid id))
            {
                throw new ConversionException(value, type);
            }

            return id;
        }

        protected override string ToString(Guid value, Type type, IConversionFactory factory) => value.ToString();
    }
}