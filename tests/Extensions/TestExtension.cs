using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;

namespace NtCore.Tests.Extensions
{
    public static class TestExtension
    {
        public static void CreateMapMock(this IClient client)
        {
            var map = new Map(0);

            map.AddEntity(new Monster
            {
                Id = 1874
            });
            map.AddEntity(new Monster
            {
                Id = 874
            });

            map.AddEntity(new Npc
            {
                Id = 2053
            });
            map.AddEntity(new Npc
            {
                Id = 1026
            });

            map.AddEntity(client.Character.As<Character>());
        }

        public static void CreateMinilandMock(this IClient client)
        {
            client.Character.As<Character>().Map = new Miniland();
        }
    }
}