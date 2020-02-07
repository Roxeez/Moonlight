using Moonlight.Game.Entities;
using Moonlight.Game.Maps;

namespace Moonlight.Tests.Extensions
{
    public static class CharacterExtension
    {
        public static Miniland SetFakeMiniland(this Character character)
        {
            character.Map = new Miniland("Miniland", new byte[0]);
            return character.Map as Miniland;
        }

        public static Map SetFakeMap(this Character character)
        {
            character.Map = new Map(1, "NosVille", new byte[0]);
            return character.Map;
        }
    }
}