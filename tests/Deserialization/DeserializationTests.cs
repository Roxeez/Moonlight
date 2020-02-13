using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core.Extensions;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Extensions;
using Moonlight.Packet.Extensions;
using NFluent;

namespace Moonlight.Tests.Deserialization
{
    public class DeserializationTests
    {
        private readonly IDeserializer _deserializer;

        protected DeserializationTests()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogger();
            services.AddPacketDependencies();

            _deserializer = services.BuildServiceProvider().GetService<IDeserializer>();
        }

        protected T Deserialize<T>(string packet) where T : class
        {
            var deserialized = _deserializer.Deserialize(packet) as T;

            Check.That(deserialized).IsNotNull();
            Check.That(deserialized).IsInstanceOf<T>();

            return deserialized;
        }
    }
}