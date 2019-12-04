
using Moq;
using NFluent;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Inventory;
using NtCore.Game.Entities;
using NtCore.Network;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class CharacterPacketTests
    {
        private readonly NtCore _ntCore;
        private readonly IClient _client;

        public CharacterPacketTests()
        {
            _ntCore = new NtCore();
            var mock = new Mock<IClient>();
            
            mock.Setup(x => x.ReceivePacket(It.IsAny<string>())).Callback((string p) =>  _ntCore.PacketManager.Handle(mock.Object, p, PacketType.Recv));
            mock.SetupGet(x => x.Character).Returns(new Character());

            _client = mock.Object;
            
            PacketHandlingTestUtility.CreateFakeMap(_client);
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
        [InlineData("pairy 1 0 0 0 0", Element.NEUTRAL, 0)]
        [InlineData("pairy 1 0 1 50 0", Element.FIRE, 50)]
        public void Pairy_Packet_Change_Character_Fairy(string packet, Element element, int power)
        {
            _client.ReceivePacket(packet);

            IFairy fairy = _client.Character.Equipment.Fairy;

            Check.That(fairy.Element).IsEqualTo(element);
            Check.That(fairy.Power).IsEqualTo(power);
        }

        [Theory]
        [InlineData("cond 1 0 0 0 10", EntityType.Player, 0, 10)]
        [InlineData("cond 2 2053 0 1 14", EntityType.Npc, 2053, 14)]
        [InlineData("cond 3 1874 1 0 8", EntityType.Monster, 1874, 8)]
        public void Cond_Packet_Change_Entity_Speed(string packet, EntityType entityType, int entityId, int speed)
        {
            _client.ReceivePacket(packet);

            ILivingEntity entity = _client.Character.Map.GetLivingEntity(entityType, entityId);

            Check.That(entity).IsNotNull();
            Check.That(entity.Speed).IsEqualTo(speed);
        }
    }
}