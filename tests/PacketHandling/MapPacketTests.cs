using System;
using Moq;
using NFluent;
using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network;
using NtCore.Tests.Extensions;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class MapPacketTests
    {
        private readonly IClient _client;

        public MapPacketTests()
        {
            var ntCore = Program.Setup();
            var mock = new Mock<IClient>();
            
            mock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback((string p) =>  ntCore.PacketManager.Handle(mock.Object, p, PacketType.Recv));
            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object));
            
            _client = mock.Object;
        }
        
        [Theory]
        [InlineData("c_map 0 150 1", 150)]
        [InlineData("c_map 0 1550 1", 1550)]
        [InlineData("c_map 0 100 1", 100)]
        public void CMap_Packet_Change_Character_Map(string packet, int mapId)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Map).IsNotNull();
            Check.That(_client.Character.Map.Id).IsEqualTo(mapId);
        }

        [Theory]
        [InlineData("in 2 322 2053 36 131 7 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 322, 2053, 36, 131, 100, 100)]
        [InlineData("in 2 150 1026 124 63 7 86 47 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 150, 1026, 124, 63, 86, 47)]
        public void In_Packet_Add_Npc_To_Map(string packet, int vnum, int id, short x, short y, byte hpPercentage, byte mpPercentage)
        {
            _client.CreateMapMock();
            
            _client.ReceivePacket(packet);

            INpc npc = _client.Character.Map.GetEntity<INpc>(id);

            Check.That(npc).IsNotNull();
            Check.That(npc.Vnum).IsEqualTo(vnum);
            Check.That(npc.Position).IsEqualTo(new Position(x, y));
            Check.That(npc.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(npc.MpPercentage).IsEqualTo(mpPercentage);

        }
        
        [Theory]
        [InlineData("in 3 24 1874 17 156 2 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 24, 1874, 17, 156, 100, 100)]
        [InlineData("in 3 294 874 54 26 2 14 87 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 294, 874, 54, 26, 14, 87)]
        public void In_Packet_Add_Monster_To_Map(string packet, int vnum, int id, short x, short y, byte hpPercentage, byte mpPercentage)
        {
            _client.CreateMapMock();
            
            _client.ReceivePacket(packet);

            IMonster monster = _client.Character.Map.GetEntity<IMonster>(id);

            Check.That(monster).IsNotNull();
            Check.That(monster.Vnum).IsEqualTo(vnum);
            Check.That(monster.Position).IsEqualTo(new Position(x, y));
            Check.That(monster.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(monster.MpPercentage).IsEqualTo(mpPercentage);
        }
        
        [Theory]
        [InlineData("in 9 2024 2553183 83 1 9 0 0 0", 2024, 2553183, 83, 1, 9)]
        [InlineData("in 9 1076 1573974 124 16 95 0 0 0", 1076, 1573974, 124, 16, 95)]
        public void In_Packet_Add_Drop_To_Map(string packet, int vnum, int id, short x, short y, int amount)
        {
            _client.CreateMapMock();
            
            _client.ReceivePacket(packet);

            IDrop drop = _client.Character.Map.GetEntity<IDrop>(id);

            Check.That(drop).IsNotNull();
            Check.That(drop.Vnum).IsEqualTo(vnum);
            Check.That(drop.Position).IsEqualTo(new Position(x, y));
            Check.That(drop.Amount).IsEqualTo(amount);
        }
    }
}