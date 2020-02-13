using System;

namespace Moonlight.Conversion.Converters
{
    public interface IConverter
    {
        Type ConversionType { get; }
        bool IsGeneric { get; }
        string ToString(object value, Type type, IConversionFactory factory);
        object ToObject(string value, Type type, IConversionFactory factory);
    }

    public abstract class Converter<T> : IConverter
    {
        public Type ConversionType => typeof(T);
        public virtual bool IsGeneric => false;

        public string ToString(object value, Type type, IConversionFactory factory) => ToString((T)value, type, factory);
        object IConverter.ToObject(string value, Type type, IConversionFactory factory) => ToObject(value, type, factory);

        protected abstract T ToObject(string value, Type type, IConversionFactory factory);
        protected abstract string ToString(T value, Type type, IConversionFactory factory);
    }
}