using NtCore.API;
using NtCore.API.Game.Maps;
using NtCore.Game.Maps;

namespace NtCore.Tests
{
    public class Utility
    {
        public static IMinilandObject[] CreateDummyMinilandObjects()
        {
            return new IMinilandObject[]
            {
                new MinilandObject
                {
                    Vnum = 3250,
                    Id = 1,
                    Position = new Position(15, 25)
                },
                new MinilandObject
                {
                    Vnum = 3285,
                    Id = 2,
                    Position = new Position(25, 65)
                },
                new MinilandObject
                {
                    Vnum = 1285,
                    Id = 3,
                    Position = new Position(65, 87)
                }
            };
        }
    }
}