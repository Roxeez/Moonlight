using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Game.Entities.Impl;
using NtCore.Game.Maps.Impl;
using NtCore.Network;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class CharacterPacketTests
    {
        private readonly IClient _client;

        public CharacterPacketTests()
        {
            var mock = new Mock<IClient>();

            mock.Setup(x => x.ReceivePacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.Instance.GetPacketManager().Handle(mock.Object, p, PacketType.Recv));
            mock.Setup(x => x.SendPacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.Instance.GetPacketManager().Handle(mock.Object, p, PacketType.Send));

            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object));

            _client = mock.Object;
        }

        [Theory]
        [InlineData("stat 2500 2000 1500 1000", 2500, 2000, 1500, 1000)]
        [InlineData("stat 1 2 3 4", 1, 2, 3, 4)]
        public void Stat_Packet_Update_Character_Stats(string packet, int hp, int maxHp, int mp, int maxMp)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Hp).IsEqualTo(hp);
            Check.That(_client.Character.Mp).IsEqualTo(mp);
            Check.That(_client.Character.MaxHp).IsEqualTo(maxHp);
            Check.That(_client.Character.MaxMp).IsEqualTo(maxMp);
        }

        [Theory]
        [InlineData("gold 1000000 1000", 1000000)]
        [InlineData("gold 1987 1000", 1987)]
        public void Gold_Packet_Update_Character_Golds(string packet, int gold)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Gold).IsEqualTo(gold);
        }

        [Theory]
        [InlineData("fs 0", Faction.NEUTRAL)]
        [InlineData("fs 1", Faction.ANGEL)]
        [InlineData("fs 2", Faction.DEMON)]
        public void Faction_Packet_Update_Character_Faction(string packet, Faction faction)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Faction).IsEqualTo(faction);
        }

        [Theory]
        [InlineData("sp 875000 1000000 8500 10000", 875000, 1000000, 8500, 10000)]
        [InlineData("sp 875000 900000 2500 100000", 875000, 900000, 2500, 100000)]
        public void Sp_Packet_Update_Character_Sp_Points(string packet, int additionalPoints,
            int maximumAdditionalPoints, int points, int maximumPoints)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.AdditionalSpPoints).IsEqualTo(additionalPoints);
            Check.That(_client.Character.MaximumAdditionalSpPoints).IsEqualTo(maximumAdditionalPoints);
            Check.That(_client.Character.SpPoints).IsEqualTo(points);
            Check.That(_client.Character.MaximumSpPoints).IsEqualTo(maximumPoints);
        }

        [Theory]
        [InlineData("cond 1 47 0 0 10", EntityType.PLAYER, 47, 10)]
        [InlineData("cond 2 2053 0 1 14", EntityType.NPC, 2053, 14)]
        [InlineData("cond 3 1874 1 0 8", EntityType.MONSTER, 1874, 8)]
        public void Cond_Packet_Change_Entity_Speed(string packet, EntityType entityType, int entityId, int speed)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, entityId).Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var entity = _client.Character.Map.GetEntity(entityType, entityId).As<ILivingEntity>();

            Check.That(entity).IsNotNull();
            Check.That(entity.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("walk 15 35 0 10", 15, 35, 10)]
        [InlineData("walk 128 11 0 6", 128, 11, 6)]
        public void Walk_Packet_Change_Character_Position_And_Speed(string packet, byte x, byte y, byte speed)
        {
            _client.SendPacket(packet);

            Check.That(_client.Character.Position).IsEqualTo(new Position(x, y));
            Check.That(_client.Character.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("mv 2 2053 18 10 21", EntityType.NPC, 2053, 18, 10, 21)]
        [InlineData("mv 3 874 87 3 14", EntityType.MONSTER, 874, 87, 3, 14)]
        public void Mv_Packet_Change_Entity_Position_And_Speed(string packet, EntityType entityType, int id, byte x, byte y, byte speed)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, id).Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            var entity = _client.Character.Map.GetEntity(entityType, id).As<ILivingEntity>();

            Check.That(entity.Position).IsEqualTo(new Position(x, y));
            Check.That(entity.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("st 2 2053 10 0 100 100 5000 2500", EntityType.NPC, 2053, 10, 100, 100, 5000, 2500)]
        [InlineData("st 3 1874 88 0 50 75 24578 8745", EntityType.MONSTER, 1874, 88, 50, 75, 24578, 8745)]
        public void St_Packet_Update_Target_Stats(string packet, EntityType entityType, int id, byte level, byte hpPercentage, byte mpPercentage, int hp, int mp)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, id).Create();
            fakeMap.AddEntity(_client.Character);
            
            _client.ReceivePacket(packet);

            ITarget target = _client.Character.Target;

            Check.That(target).IsNotNull();
            Check.That(target.Hp).IsEqualTo(hp);
            Check.That(target.Mp).IsEqualTo(mp);
            Check.That(target.Entity.Level).IsEqualTo(level);
            Check.That(target.Entity.EntityType).IsEqualTo(entityType);
            Check.That(target.Entity.Id).IsEqualTo(id);
            Check.That(target.Entity.HpPercentage).IsEqualTo(hpPercentage);
            Check.That(target.Entity.MpPercentage).IsEqualTo(mpPercentage);
        }

        [Theory]
        [InlineData("c_info Roxeetest - -1 -1 - 1290125 0 1 0 9 0 1 0 0 0 0 0 0 0", "Roxeetest", 1290125, Gender.FEMALE, ClassType.ADVENTURER)]
        [InlineData("c_info Roxeez - -1 -1 - 999999 0 0 0 9 1 1 0 0 0 0 0 0 0", "Roxeez", 999999, Gender.MALE, ClassType.SWORDSMAN)]
        public void CInfo_Packet_Initialize_Character(string packet, string name, int id, Gender gender, ClassType classType)
        {
            
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Name).IsEqualTo(name);
            Check.That(_client.Character.Id).IsEqualTo(id);
            Check.That(_client.Character.Gender).IsEqualTo(gender);
            Check.That(_client.Character.Class).IsEqualTo(classType);
        }

        [Theory]
        [InlineData("lev 5 3840 3 1380 5460 3600 0 6 0 0 3 0", 5, 3)]
        [InlineData("lev 55 3840 28 1380 5460 3600 0 6 0 0 3 0", 55, 28)]
        public void Lev_Packet_Change_Character_Level(string packet, int level, int jobLevel)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Level).IsEqualTo(level);
            Check.That(_client.Character.JobLevel).IsEqualTo(jobLevel);
        }
    }
}