using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Extensions
{
    public static class CastExtensions
    {
        public static Character AsModifiable(this ICharacter character)
        {
            return character as Character;
        }

        public static Map AsModifiable(this IMap map)
        {
            return map as Map;
        }
    }
}