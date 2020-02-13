using Moonlight.Core;
using Moonlight.Core.Enums.Game;
using Moonlight.Database.Dto;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Handling
{
    public class CharacterHandlingTests : PacketHandlingTest
    {
        [Fact]
        public void Cond_Packet_Change_Entity_Speed()
        {
            Map fakeMap = Character.SetFakeMap();

            var monster = new Monster(55, new MonsterDto(), "dummy");
            fakeMap.AddEntity(monster);

            Client.ReceivePacket("cond 3 55 0 0 10");

            Check.That(monster.Speed).Is<byte>(10);
        }

        [Fact]
        public void Faction_Packet_Update_Character_Faction()
        {
            Client.ReceivePacket("fs 1");

            Check.That(Character.Faction).Is(FactionType.ANGEL);
        }

        [Fact]
        public void Ski_Packet_Set_Character_Skills()
        {
            Client.ReceivePacket("ski 833 833 833 834 835 836 837 838 839 840 841 21 25 37 45 353 356");

            Check.That(Client.Character.Skills).HasElementThatMatches(x => x.Id == 833);
            Check.That(Client.Character.Skills).HasElementThatMatches(x => x.Id == 837);
        }

        [Fact]
        public void Sp_Packet_Update_Character_Sp_Points()
        {
            Client.ReceivePacket("sp 875000 1000000 8500 10000");

            Check.That(Character.SpPoints).Is(8500);
            Check.That(Character.AdditionalSpPoints).Is(875000);
        }

        [Fact]
        public void Stat_Packet_Set_Character_Stat()
        {
            Client.ReceivePacket("stat 2000 3000 1000 1500");

            Check.That(Character.Hp).Is(2000);
            Check.That(Character.MaxHp).Is(3000);
            Check.That(Character.Mp).Is(1000);
            Check.That(Character.MaxMp).Is(1500);
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