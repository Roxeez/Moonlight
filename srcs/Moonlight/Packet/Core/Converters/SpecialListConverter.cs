using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class SpecialListConverter : Converter<IList>
    {
        private readonly IReflectionCache _reflectionCache;

        public SpecialListConverter(IReflectionCache reflectionCache) => _reflectionCache = reflectionCache;

        public override bool IsGeneric => true;

        protected override IList ToObject(string value, Type type, IConversionFactory factory)
        {
            Type generic = type.GenericTypeArguments.FirstOrDefault();
            Type listType = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(listType);

            char separator = ' ';
            if (typeof(IPacket).IsAssignableFrom(listType))
            {
                separator = '^';
            }

            string[] values = value.Split(separator);
            foreach (string entry in values)
            {
                list.Add(factory.ToObject(entry, generic));
            }

            return list;
        }

        protected override string ToString(IList value, Type type, IConversionFactory factory)
        {
            Type valueType = value.GetType().GenericTypeArguments.FirstOrDefault();
            var sb = new StringBuilder();
            foreach (object entry in value)
            {
                sb.Append(factory.ToString(entry, valueType)).Append(" ");
            }

            return sb.ToString().TrimEnd();
        }
    }
}