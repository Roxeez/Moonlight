using NtCore.API.Game.Entities;
using NtCore.API.Game.Inventory;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;
using NtCore.Game.Inventory;
using NtCore.Game.Maps;

namespace NtCore.Extensions
{
    public static class CastExtensions
    {
        public static Character AsModifiable(this ICharacter character)
        {
            return character.AsModifiable<Character>();
        }
        
        public static Map AsModifiable(this IMap map)
        {
            return map.AsModifiable<Map>();
        }
        
        public static Equipment AsModifiable(this IEquipment equipment)
        {
            return equipment.AsModifiable<Equipment>();
        }
        
        private static TDestination AsModifiable<TDestination>(this object source) where TDestination : class
        {
            return source as TDestination;
        }
    }
}