using Moonlight.Core;
using Moonlight.Tests.Extensions;
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

        [Fact]
        public void Walk_Packet_Set_Last_Movement()
        {
            Client.ReceivePacket("walk 140 87 1 12");
            
            Check.That(Client.Character.Position).Is(new Position(140, 87));
            Check.That(Client.Character.LastMovement).Not.IsDefaultValue();
        }
    }
}