using Moonlight.Core.Enums;
using Moonlight.Packet.Battle;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Deserialization
{
    public class BattlePacketDeserializationTests : DeserializationTests
    {
        [Fact]
        public void St_Packet()
        {
            SrPacket packet = Deserialize<SrPacket>("sr 6");

            Check.That(packet.CastId).Is(6);
        }

        [Fact]
        public void Su_Packet()
        {
            SuPacket packet = Deserialize<SuPacket>("su 1 12345 3 2080 240 8 11 257 0 0 0 0 723 0 0");
            
            Check.That(packet.EntityType).Is(EntityType.PLAYER);
            Check.That(packet.EntityId).Is(12345);
            Check.That(packet.TargetEntityType).Is(EntityType.MONSTER);
            Check.That(packet.TargetEntityId).Is(2080);
            Check.That(packet.SkillVnum).Is(240);
            Check.That(packet.Damage).Is(723);
        }
    }
}