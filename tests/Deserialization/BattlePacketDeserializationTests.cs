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
    }
}