using NFluent;
using Xunit;

namespace Moonlight.Tests.Handling
{
    public class CharacterHandlingTests : PacketHandlingTest
    {
        [Fact]
        public void Ski_Packet_Set_Character_Skills()
        {
            Client.ReceivePacket("ski 833 833 833 834 835 836 837 838 839 840 841 21 25 37 45 353 356");

            Check.That(Client.Character.Skills).HasElementThatMatches(x => x.Id == 833);
            Check.That(Client.Character.Skills).HasElementThatMatches(x => x.Id == 356);
        }
    }
}