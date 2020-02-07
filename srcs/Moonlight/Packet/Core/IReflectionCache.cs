using System;

namespace Moonlight.Packet.Core
{
    internal interface IReflectionCache
    {
        CachedType GetCachedType(string packetHeader);
        CachedType GetCachedType(Type type);
    }
}