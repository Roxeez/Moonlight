using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps;
using NtCore.Game.Maps.Impl;
using NtCore.Network;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class MapPacketTests
    {
        private readonly IClient _client;

        public MapPacketTests()
        {
            var mock = new Mock<IClient>();

            mock.Setup(x => x.ReceivePacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.GetPacketManager().Handle(mock.Object, p, PacketType.Recv));
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
        [InlineData("in 2 322 2053 36 131 7 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 322, 2053, 36, 131, 100,
            100, 7)]
        [InlineData("in 2 150 1026 124 63 6 86 47 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 150, 1026, 124, 63, 86, 47,
            6)]
        public void In_Packet_Add_Npc_To_Map(string packet, int vnum, int id, short x, short y, byte hpPercentage,
            byte mpPercentage, byte direction)
        {
            Map fakeMap = new MapBuilder().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var npc = _client.Character.Map.GetEntity<INpc>(id);

            Check.That(npc).IsNotNull();
            Check.That(npc.Vnum).IsEqualTo(vnum);
            Check.That(npc.Position).IsEqualTo(new Position(x, y));
            Check.That(npc.Direction).IsEqualTo(direction);
            Check.That(npc.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(npc.MpPercentage).IsEqualTo(mpPercentage);
        }

        [Theory]
        [InlineData("in 3 24 1874 17 156 2 100 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 24, 1874, 17, 156, 100, 100,
            2)]
        [InlineData("in 3 294 874 54 26 3 14 87 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0", 294, 874, 54, 26, 14, 87, 3)]
        public void In_Packet_Add_Monster_To_Map(string packet, int vnum, int id, short x, short y, byte hpPercentage,
            byte mpPercentage, byte direction)
        {
            Map fakeMap = new MapBuilder().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var monster = _client.Character.Map.GetEntity<IMonster>(id);

            Check.That(monster).IsNotNull();
            Check.That(monster.Vnum).IsEqualTo(vnum);
            Check.That(monster.Position).IsEqualTo(new Position(x, y));
            Check.That(monster.Direction).IsEqualTo(direction);
            Check.That(monster.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(monster.MpPercentage).IsEqualTo(mpPercentage);
        }

        [Theory]
        [InlineData(
            "in 1 Cyborg-265 - 892492 3 3 2 0 1 0 9 0 -1.12.1.8.-1.-1.-1.-1.-1.-1 100 100 0 -1 0 0 0 0 0 0 0 0 -1 - 1 0 0 0 0 1 0 0|0|0 0 0 10 0 0",
            "Cyborg-265", 892492, 3, 3, 2, Gender.FEMALE, ClassType.ADVENTURER, 100, 100, 1)]
        [InlineData(
            "in 1 Ronflisse - 1299439 75 10 2 0 0 1 3 1 -1.13.2.8.-1.-1.-1.-1.-1.-1 80 10 0 -1 0 0 0 0 0 0 1 1 -1 - 1 0 0 0 0 7 0 0|0|0 0 0 10 0 0",
            "Ronflisse", 1299439, 75, 10, 2, Gender.MALE, ClassType.SWORDSMAN, 80, 10, 7)]
        public void In_Packet_Add_Player_To_Map(string packet, string name, int id, byte x, byte y, byte direction,
            Gender gender, ClassType classType, byte hpPercentage, byte mpPercentage, byte level)
        {
            Map fakeMap = new MapBuilder().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var player = _client.Character.Map.GetEntity<IPlayer>(id);

            Check.That(player).IsNotNull();
            Check.That(player.Name).IsEqualTo(name);
            Check.That(player.Id).IsEqualTo(id);
            Check.That(player.Position).IsEqualTo(new Position(x, y));
            Check.That(player.Direction).IsEqualTo(direction);
            Check.That(player.Gender).IsEqualTo(gender);
            Check.That(player.Class).IsEqualTo(classType);
            Check.That(player.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(player.MpPercentage).IsEqualTo(mpPercentage);
            Check.That(player.Level).IsEqualTo(level);
        }

        [Theory]
        [InlineData("in 9 2024 2553183 83 1 9 0 0 0", 2024, 2553183, 83, 1, 9)]
        [InlineData("in 9 1076 1573974 124 16 95 0 0 0", 1076, 1573974, 124, 16, 95)]
        public void In_Packet_Add_Drop_To_Map(string packet, int vnum, int id, short x, short y, int amount)
        {
            Map fakeMap = new MapBuilder().Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var drop = _client.Character.Map.GetEntity<IDrop>(id);

            Check.That(drop).IsNotNull();
            Check.That(drop.Vnum).IsEqualTo(vnum);
            Check.That(drop.Position).IsEqualTo(new Position(x, y));
            Check.That(drop.Amount).IsEqualTo(amount);
        }

        [Theory]
        [InlineData("out 1 9854", EntityType.PLAYER, 9854)]
        [InlineData("out 2 2053", EntityType.NPC, 2053)]
        [InlineData("out 2 876", EntityType.NPC, 876)]
        [InlineData("out 3 1874", EntityType.MONSTER, 1874)]
        public void Out_Packet_Remove_Entity_From_Map(string packet, EntityType entityType, int id)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, id).Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            IMap map = _client.Character.Map;

            Check.That(map.GetEntity(entityType, id)).IsNull();
        }

        [Theory]
        [InlineData("drop 1012 317060 52 163 1 0 1290125", 1012, 317060, 52, 163, 1, 1290125, false)]
        [InlineData("drop 1046 317117 49 20 18 0 1299318", 1046, 317117, 49, 20, 18, 1299318, true)]
        public void Drop_Packet_Add_Drop_To_Map(string packet, int vnum, int dropId, short x, short y, int amount, int ownerId, bool isGold)
        {
            Map fakeMap = new MapBuilder().WithPlayers(1290125, 1299318).Create();
            
            fakeMap.AddEntity(_client.Character);
            _client.ReceivePacket(packet);
            
            IMap map = _client.Character.Map;

            var drop = map.GetEntity<IDrop>(dropId);
            var owner = map.GetEntity<IPlayer>(ownerId);
            
            Check.That(drop).IsNotNull();
            Check.That(drop.Vnum).IsEqualTo(vnum);
            Check.That(drop.Position).IsEqualTo(new Position(x, y));
            Check.That(drop.Amount).IsEqualTo(amount);
            Check.That(drop.Owner).IsEqualTo(owner);
            Check.That(drop.IsGold).IsEqualTo(isGold);
        }

        [Theory]
        [InlineData("get 1 252525 12645 0", EntityType.PLAYER, 252525, 12645)]
        [InlineData("get 1 1294576 478569 0", EntityType.PLAYER, 1294576, 478569)]
        public void Get_Packet_Remove_Drop_From_Map(string packet, EntityType entityType, int entityId, int dropId)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, entityId).WithDrops(dropId).Create();
            
            fakeMap.AddEntity(_client.Character);
            _client.ReceivePacket(packet);
            
            Check.That(fakeMap.GetEntity<IDrop>(dropId)).IsNull();
        }
    }
}