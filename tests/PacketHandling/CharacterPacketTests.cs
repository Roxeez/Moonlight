using Microsoft.Extensions.DependencyInjection;
using Moq;
using NFluent;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Extensions;
using NtCore.API.Game.Entities;
using NtCore.Core;
using NtCore.Game.Entities;
using NtCore.Network;
using NtCore.Tests.Extensions;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class CharacterPacketTests
    {
        private readonly IClient _client;

        public CharacterPacketTests()
        {
            var packetManager = Program.Setup().GetService<IPacketManager>();
            var mock = new Mock<IClient>();
            
            mock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback((string p) =>  packetManager.Handle(mock.Object, p, PacketType.Recv));
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
        public void Sp_Packet_Update_Character_Sp_Points(string packet, int additionalPoints, int maximumAdditionalPoints, int points, int maximumPoints)
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

            ILivingEntity entity = _client.Character.Map.GetEntity(entityType, entityId).As<ILivingEntity>();

            Check.That(entity).IsNotNull();
            Check.That(entity.Speed).IsEqualTo(speed);
        }
    }
}