using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Maps;

namespace NtCore.Tests.Extensions
{
    public static class TestExtension
    {
        public static void CreateMapMock(this IClient client)
        {
            Map map = new Map(0);
            
            map.AddMonster(new Monster
            {
                Id = 1874
            });
            map.AddMonster(new Monster
            {
                Id = 874
            });
            
            map.AddNpc(new Npc
            {
                Id = 2053
            });
            map.AddNpc(new Npc
            {
                Id = 1026
            });
            
            map.AddPlayer(client.Character.As<Character>());
        }

        public static void CreateMinilandMock(this IClient client)
        {
            client.Character.As<Character>().Map = new Miniland();
        }
    }
}