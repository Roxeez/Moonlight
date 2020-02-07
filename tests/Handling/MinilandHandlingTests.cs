using System.Diagnostics;
using System.Linq;
using Moonlight.Game.Maps;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Handling
{
    public class MinilandHandlingTests : PacketHandlingTest
    {
        [Fact]
        public void CMap_Packet_Set_Miniland_When_Id_Is_20001()
        {
            Client.ReceivePacket("c_map 1 20001 1");

            Check.That(Character.Map).IsInstanceOf<Miniland>();
        }

        [Fact]
        public void MlInfoBr_Packet_Set_Miniland_Owner()
        {
            Miniland miniland = Character.SetFakeMiniland();
            Client.ReceivePacket("mlinfobr 3800 *bliblou* 2 343 10 Cc^tlm");

            Check.That(miniland.Owner).Is("*bliblou*");
        }

        [Fact]
        public void MltObj_Packet_Set_Miniland_Objects()
        {
            Miniland miniland = Character.SetFakeMiniland();
            Client.ReceivePacket("mltobj 3210.5.17.2 3005.6.31.3 3187.7.7.14 3185.8.2.15 3128.9.5.21 3125.10.5.27 3127.11.18.27 3126.12.18.21");

            Trace.WriteLine(string.Join(", ", miniland.Objects.Select(x => x.Item.Vnum)));
            Check.That(miniland.Objects).HasElementThatMatches(x => x.Item.Vnum == 3210);
            Check.That(miniland.Objects).HasElementThatMatches(x => x.Item.Vnum == 3005);
            Check.That(miniland.Objects.First(x => x.Item.Vnum == 3125)).IsInstanceOf<Minigame>();
        }
    }
}