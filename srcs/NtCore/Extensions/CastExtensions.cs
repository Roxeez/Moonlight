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

        public static Monster AsModifiable(this IMonster monster)
        {
            return monster.AsModifiable<Monster>();
        }

        public static Npc AsModifiable(this INpc npc)
        {
            return npc.AsModifiable<Npc>();
        }

        public static Player AsModifiable(this IPlayer player)
        {
            return player.AsModifiable<Player>();
        }

        public static TDestination AsModifiable<TDestination>(this object source) where TDestination : class
        {
            return source as TDestination;
        }
    }
}