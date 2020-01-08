using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class BattlePacketTests
    {
        private const int CharacterId = 99999;
        private readonly IClient _client;
        
        public BattlePacketTests()
        {
            var mock = new Mock<IClient>();

            mock.Setup(x => x.ReceivePacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.GetPacketManager().Handle(mock.Object, p, PacketType.Recv));
            mock.Setup(x => x.SendPacket(It.IsAny<string>()))
                .Callback((string p) => NtCoreAPI.GetPacketManager().Handle(mock.Object, p, PacketType.Send));

            mock.SetupGet(x => x.Character).Returns(new Character(mock.Object)
            {
                Id = CharacterId
            });

            _client = mock.Object;
        }

        [Theory]
        [InlineData("st 3 1 1 1 100 100 5000 5000", EntityType.MONSTER, 1, 100, 100, 5000, 5000)]
        [InlineData("st 3 124 1 1 74 98 14250 1987", EntityType.MONSTER, 124, 74, 98, 14250, 1987)]
        public void St_Packet_Update_Target(string packet, EntityType entityType, int entityId, int hpPercent, int mpPercent, int hp, int mp)
        {
            Character character = _client.Character;
            
            Map map = new MapBuilder().WithEntity(entityType, entityId).Create();
            map.AddEntity(character);

            character.Target = new Target(map.GetEntity<LivingEntity>(entityType, entityId));
            
            _client.ReceivePacket(packet);

            Check.That(character.Target.Hp).IsEqualTo(hp);
            Check.That(character.Target.Mp).IsEqualTo(mp);
            Check.That(character.Target.Entity.HpPercentage).IsEqualTo(hpPercent);
            Check.That(character.Target.Entity.MpPercentage).IsEqualTo(mpPercent);
        }

        [Theory]
        [InlineData("su 1 99999 3 1 250 10 0 0 0 0 1 50 1000 1 1", EntityType.MONSTER, 1, 50)]
        [InlineData("su 1 99999 3 50 250 10 0 0 0 0 1 78 1000 1 1", EntityType.MONSTER, 50, 78)]
        public void Su_Packet_Update_Entity_Hp(string packet, EntityType entityType, int entityId, int hpPercentage)
        {
            Character character = _client.Character;
            
            Map map = new MapBuilder().WithEntity(entityType, entityId).Create();
            map.AddEntity(character);
            _client.ReceivePacket(packet);

            LivingEntity entity = map.GetEntity<LivingEntity>(entityType, entityId);
            Check.That(entity).IsNotNull();
            Check.That(entity?.HpPercentage).IsEqualTo(hpPercentage);
        }

        [Theory]
        [InlineData("ncif 3 100", EntityType.MONSTER, 100)]
        [InlineData("ncif 1 999", EntityType.PLAYER, 999)]
        public void Ncif_Packet_Set_Target(string packet, EntityType entityType, int id)
        {
            Character character = _client.Character;

            Map map = new MapBuilder().WithEntity(entityType, id).Create();
            map.AddEntity(character);

            _client.SendPacket(packet);

            Check.That(character.Target).IsNotNull();
            Check.That(character.Target.Entity.EntityType).IsEqualTo(entityType);
            Check.That(character.Target.Entity.Id).IsEqualTo(id);
        }
    }
}