using Microsoft.Extensions.DependencyInjection;
using Moq;
using NFluent;
using NtCore.API.Clients;
using NtCore.API.Enums;
using NtCore.API.Extensions;
using NtCore.API.Game.Battle;
using NtCore.API.Game.Entities;
using NtCore.Game.Entities;
using NtCore.Network;
using NtCore.Tests.Extensions;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class CharacterPacketTests
    {
        public CharacterPacketTests()
        {
            var packetManager = Program.UnitTestProvider().GetService<IPacketManager>();
            var mock = new Mock<IClient>();

            mock.Setup(x => x.ReceivePacket(It.IsAny<string>()))
                .Callback((string p) => packetManager.Handle(mock.Object, p, PacketType.Recv));
            mock.Setup(x => x.SendPacket(It.IsAny<string>()))
                .Callback((string p) => packetManager.Handle(mock.Object, p, PacketType.Send));

            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object));

            _client = mock.Object;
        }

        private readonly IClient _client;

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
        [InlineData("cond 1 0 0 0 10", EntityType.PLAYER, 0, 10)]
        [InlineData("cond 2 2053 0 1 14", EntityType.NPC, 2053, 14)]
        [InlineData("cond 3 1874 1 0 8", EntityType.MONSTER, 1874, 8)]
        public void Cond_Packet_Change_Entity_Speed(string packet, EntityType entityType, int entityId, int speed)
        {
            _client.CreateMapMock();

            _client.ReceivePacket(packet);

            var entity = _client.Character.Map.GetEntity(entityType, entityId).As<ILivingEntity>();

            Check.That(entity).IsNotNull();
            Check.That(entity.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("st 2 2053 10 0 100 100 5000 2500", EntityType.NPC, 2053, 10, 100, 100, 5000, 2500)]
        [InlineData("st 3 1874 88 0 50 75 24578 8745", EntityType.MONSTER, 1874, 88, 50, 75, 24578, 8745)]
        public void St_Packet_Update_Target_Stats(string packet, EntityType entityType, int id, byte level, byte hpPercentage, byte mpPercentage, int hp, int mp)
        {
            _client.CreateMapMock();
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

        [Fact]
        public void CInfo_Packet_Initialize_Character()
        {
            _client.ReceivePacket("c_info Roxeetest - -1 -1 - 1290125 0 1 0 9 0 1 0 0 0 0 0 0 0");

            Check.That(_client.Character.Name).IsEqualTo("Roxeetest");
            Check.That(_client.Character.Id).IsEqualTo(1290125);
            Check.That(_client.Character.Gender).IsEqualTo(Gender.FEMALE);
            Check.That(_client.Character.Class).IsEqualTo(ClassType.ADVENTURER);
        }

        [Fact]
        public void Lev_Packet_Change_Character_Level()
        {
            _client.ReceivePacket("lev 5 3840 3 1380 5460 3600 0 6 0 0 3 0");

            Check.That(_client.Character.Level).IsEqualTo(5);
            Check.That(_client.Character.JobLevel).IsEqualTo(3);
        }
    }
}