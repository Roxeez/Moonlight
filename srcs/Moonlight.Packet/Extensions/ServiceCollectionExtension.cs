using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Conversion;
using Moonlight.Conversion.Converters;
using Moonlight.Core.Extensions;
using Moonlight.Packet.Core;
using Moonlight.Packet.Core.Converters;
using Moonlight.Packet.Core.Serialization;

namespace Moonlight.Packet.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddPacketDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IConversionFactory, ConversionFactory>();
            services.AddSingleton<IReflectionCache, ReflectionCache>();
            services.AddImplementingTypes<IConverter>();
            services.AddImplementingTypes<IConverter>(typeof(PacketConverter).Assembly);
            services.AddTransient<IDeserializer, Deserializer>();
        }
    }
}