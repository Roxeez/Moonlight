using System;
using System.Collections.Generic;
using System.Linq;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Utility.Conversion
{
    public class ConversionFactory : IConversionFactory
    {
        private readonly Dictionary<Type, IConverter> _converters;
        private readonly List<IConverter> _genericConverters;

        /// <summary>
        ///     Create a new instance of ConversionFactory with all default converters & all specified converters
        /// </summary>
        public ConversionFactory(IEnumerable<IConverter> converters)
        {
            _converters = new Dictionary<Type, IConverter>();
            _genericConverters = new List<IConverter>();

            foreach (IConverter converter in converters)
            {
                AddConverter(converter);
            }
        }

        public object ToObject(string value, Type type)
        {
            IConverter converter = GetConverter(type);
            if (converter == null)
            {
                throw new InvalidOperationException($"No converter found for type {type.Name}");
            }

            return converter.ToObject(value, type, this);
        }

        public string ToString(object value, Type type)
        {
            IConverter converter = GetConverter(type);
            if (converter == null)
            {
                throw new InvalidOperationException($"No converter found for type {type.Name}");
            }

            return converter.ToString(value, type, this);
        }

        private void AddConverter(IConverter converter)
        {
            if (converter.IsGeneric)
            {
                if (_genericConverters.Any(x => x.ConversionType == converter.ConversionType))
                {
                    return;
                }

                _genericConverters.Add(converter);
                return;
            }

            _converters[converter.ConversionType] = converter;
        }

        private IConverter GetConverter(Type type)
        {
            if (_converters.TryGetValue(type, out IConverter converter))
            {
                return converter;
            }

            return _genericConverters.FirstOrDefault(x => x.ConversionType.IsAssignableFrom(type));
        }
    }
}