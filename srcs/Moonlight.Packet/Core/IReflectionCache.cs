using System;

namespace Moonlight.Packet.Core
{
    public interface IReflectionCache
    {
        CachedType GetCachedType(string packetHeader);
        CachedType GetCachedType(Type type);
    }
}