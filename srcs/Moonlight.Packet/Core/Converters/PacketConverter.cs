using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Conversion;
using Moonlight.Conversion.Converters;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Core.Converters
{
    public class PacketConverter : Converter<IPacket>
    {
        private readonly IReflectionCache _reflectionCache;

        public PacketConverter(IReflectionCache reflectionCache) => _reflectionCache = reflectionCache;

        public override bool IsGeneric => true;

        protected override IPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            CachedType cachedType = _reflectionCache.GetCachedType(type);
            if (cachedType == null)
            {
                throw new ConversionException(value, type);
            }

            string[] split = value.Split(' ');
            var packet = (IPacket)cachedType.Constructor.DynamicInvoke();

            foreach (PropertyData property in cachedType.Properties)
            {
                PacketIndexAttribute indexAttribute = property.PacketIndexAttribute;
                if (indexAttribute.Index > split.Length)
                {
                    throw new ConversionException(value, type);
                }

                string content = split[indexAttribute.Index];

                if (typeof(List<>).IsAssignableFrom(property.PropertyType))
                {
                    Type genericType = property.PropertyType.GenericTypeArguments[0];
                    if (typeof(IPacket).IsAssignableFrom(genericType))
                    {
                        content = content.Replace(property.PacketIndexAttribute.Separator, "^");
                    }
                }

                var converted = factory.ToObject(content, property.PropertyType);
                if (converted is string str)
                {
                    converted = str.Replace("^", " ");
                }

                property.Setter.DynamicInvoke(packet, converted);
            }

            return packet;
        }

        protected override string ToString(IPacket value, Type type, IConversionFactory factory)
        {
            var output = new StringBuilder();
            CachedType cachedType = _reflectionCache.GetCachedType(type);

            if (cachedType == null)
            {
                throw new InvalidOperationException($"Unable to resolved packet {type.Name}");
            }

            foreach (PropertyData property in cachedType.Properties)
            {
                PacketIndexAttribute indexAttribute = property.PacketIndexAttribute;

                object obj = property.Getter.DynamicInvoke(value);
                string content = factory.ToString(obj, property.PropertyType);

                if (property.PacketIndexAttribute.Separator != null)
                {
                    content = content.Replace(" ", property.PacketIndexAttribute.Separator);
                }

                output.Append(content).Append(indexAttribute.Separator);
            }

            return output.ToString().TrimEnd();
        }
    }
}