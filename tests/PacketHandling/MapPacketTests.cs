using NFluent;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Game.Entities;
using NtCore.Extensions;
using NtCore.Game.Maps;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class MapPacketTests
    {
        private readonly NtCoreManager _ntCoreManager;
        private readonly IClient _client;

        public MapPacketTests()
        {
            _ntCoreManager = new NtCoreManager();
            _client = _ntCoreManager.CreateClient();
        }
        
        [Fact]
        public void CMap_Packet_Change_Character_Map()
        {
            _client.ReceivePacket("c_map 0 150 1");

            Check.That(_client.Character.Map).IsNotNull();
            Check.That(_client.Character.Map.Id).IsEqualTo(150);
            
            _client.ReceivePacket("c_map 0 114 1");
            
            Check.That(_client.Character.Map).IsNotNull();
            Check.That(_client.Character.Map.Id).IsEqualTo(114);
        }

        [Fact]
        public void In_Packet_2_Add_Npc_To_Map()
        {
            _client.Character.AsModifiable().Map = new Map(0);

            _client.ReceivePacket("in 2 322 2053 36 131 7 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");

            INpc npc = _client.Character.Map.GetNpc(2053);

            Check.That(npc).IsNotNull();
            Check.That(npc.Vnum).IsEqualTo(322);
            Check.That(npc.Position).IsEqualTo(new Position(36, 131));
        }
        
        [Fact]
        public void In_Packet_3_Add_Monster_To_Map()
        {
            _client.Character.AsModifiable().Map = new Map(0);

            _client.ReceivePacket("in 3 24 1874 17 156 2 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");

            IMonster monster = _client.Character.Map.GetMonster(1874);
            
            Check.That(monster).IsNotNull();
            Check.That(monster.Vnum).IsEqualTo(24);
            Check.That(monster.Position).IsEqualTo(new Position(17, 156));
        }
        
        [Fact]
        public void In_Packet_9_Add_Drop_To_Map()
        {
            _client.Character.AsModifiable().Map = new Map(0);

            _client.ReceivePacket("in 9 2024 2553183 83 1 9 0 0 0");

            IDrop drop = _client.Character.Map.GetDrop(2553183);
            
            Check.That(drop).IsNotNull();
            Check.That(drop.Vnum).IsEqualTo(2024);
            Check.That(drop.Position).IsEqualTo(new Position(83, 1));
        }
    }
}