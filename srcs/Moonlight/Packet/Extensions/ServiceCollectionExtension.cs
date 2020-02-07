using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core.Extensions;
using Moonlight.Packet.Core;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Extensions
{
    internal static class ServiceCollectionExtension
    {
        public static void AddPacketDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IConversionFactory, ConversionFactory>();
            services.AddSingleton<IReflectionCache, ReflectionCache>();
            services.AddImplementingTypes<IConverter>();
            services.AddTransient<IDeserializer, Deserializer>();
        }
    }
}