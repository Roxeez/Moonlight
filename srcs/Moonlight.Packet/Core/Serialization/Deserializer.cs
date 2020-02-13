using System;
using Moonlight.Conversion;
using Moonlight.Core.Logging;

namespace Moonlight.Packet.Core.Serialization
{
    public class Deserializer : IDeserializer
    {
        private readonly IConversionFactory _conversionFactory;
        private readonly ILogger _logger;
        private readonly IPacketReader _packetReader;
        private readonly IReflectionCache _reflectionCache;

        public Deserializer(ILogger logger, IReflectionCache reflectionCache, IConversionFactory conversionFactory)
        {
            _logger = logger;
            _packetReader = new PacketReader();
            _reflectionCache = reflectionCache;
            _conversionFactory = conversionFactory;
        }

        public IPacket Deserialize(string packet)
        {
            PacketOutput output = _packetReader.Read(packet);
            CachedType cachedType = _reflectionCache.GetCachedType(output.Header);

            if (string.IsNullOrEmpty(output.Header))
            {
                throw new InvalidOperationException("Failed to deserialize packet");
            }

            char firstChar = output.Header[0];
            if (firstChar == '$' || firstChar == '%')
            {
                string name = output.Header.Remove(0);
                string[] args = output.Content.Split(' ');

                _logger.Debug($"[DESERIALIZER] Deserialized Command packet [Header: {firstChar} / Name {name}]");
                return new CommandPacket
                {
                    Header = $"{firstChar}",
                    Content = output.Content,
                    Name = name,
                    Arguments = args
                };
            }

            if (cachedType == null)
            {
                _logger.Debug($"[DESERIALIZER] No type found in cache for header {output.Header}");
                return new UnknownPacket
                {
                    Header = output.Header,
                    Content = packet
                };
            }

            var deserialized = (IPacket)_conversionFactory.ToObject(output.Content, cachedType.PacketType);

            deserialized.Header = output.Header;
            deserialized.Content = output.Content;

            _logger.Debug($"[DESERIALIZER] {output.Header} successfully deserialized to {deserialized.GetType()}");
            return deserialized;
        }
    }
}