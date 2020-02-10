using System.Collections.Generic;
using System.Linq;
using Moonlight.Game.Entities;

namespace Moonlight.Game.Extensions
{
    public static class CharacterExtension
    {
        public static Monster GetClosestMonster(this Character character)
        {
            return character.Map.Monsters.OrderBy(x => x.Position.GetDistance(character.Position)).FirstOrDefault();
        }

        public static Monster GetClosestMonster(this Character character, int vnum)
        {
            return character.Map.Monsters.Where(x => x.Vnum == vnum).OrderBy(x => x.Position.GetDistance(character.Position)).FirstOrDefault();
        }

        public static Monster GetClosestMonsterInRadius(this Character character, int vnum, int radius)
        {
            return character.Map.Monsters.Where(x => x.Vnum == vnum).Where(x => x.Position.IsInRange(character.Position, radius)).OrderBy(x => x.Position.GetDistance(character.Position))
                .FirstOrDefault();
        }

        public static IEnumerable<Monster> GetClosestMonsters(this Character character)
        {
            return character.Map.Monsters.OrderBy(x => x.Position.GetDistance(character.Position));
        }

        public static IEnumerable<Monster> GetClosestMonsters(this Character character, int vnum)
        {
            return character.Map.Monsters.Where(x => x.Vnum == vnum).OrderBy(x => x.Position.GetDistance(character.Position));
        }

        public static IEnumerable<Monster> GetClosestMonstersInRadius(this Character character, int vnum, int radius)
        {
            return character.Map.Monsters.Where(x => x.Vnum == vnum).Where(x => x.Position.IsInRange(character.Position, radius)).OrderBy(x => x.Position.GetDistance(character.Position));
        }
    }
}