using Moonlight.Core.Enums;
using Moonlight.Packet.Map;
using Moonlight.Tests.Extensions;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Deserialization
{
    public class MapPacketDeserializationTests : DeserializationTests
    {
        [Fact]
        public void CMap_Packet()
        {
            CMapPacket packet = Deserialize<CMapPacket>("c_map 1 20001 1");

            Check.That(packet.MapId).IsEqualTo(20001);
            Check.That(packet.IsBaseMap).IsTrue();
        }

        [Fact]
        public void Drop_Packet()
        {
            DropPacket packet = Deserialize<DropPacket>("drop 150 999 15 27 4 0 123456");

            Check.That(packet.VNum).Is(150);
            Check.That(packet.DropId).Is(999);
            Check.That(packet.PositionX).Is<short>(15);
            Check.That(packet.PositionY).Is<short>(27);
            Check.That(packet.Amount).Is<short>(4);
            Check.That(packet.OwnerId).Is(123456);
        }

        [Fact]
        public void Get_Packet()
        {
            GetPacket packet = Deserialize<GetPacket>("get 1 123456 999 0");

            Check.That(packet.EntityType).Is(EntityType.PLAYER);
            Check.That(packet.EntityId).Is(123456);
            Check.That(packet.DropId).Is(999);
        }

        [Fact]
        public void Gp_Packet()
        {
            GpPacket packet = Deserialize<GpPacket>("gp 2 123 3 -1 0 0");

            Check.That(packet.SourceX).Is<short>(2);
            Check.That(packet.SourceY).Is<short>(123);
            Check.That(packet.DestinationId).Is<short>(3);
            Check.That(packet.PortalType).Is(PortalType.MAP_PORTAL);
            Check.That(packet.PortalId).Is(0);
        }

        [Fact]
        public void In_Packet_As_Drop()
        {
            InPacket packet = Deserialize<InPacket>("in 9 503 4808544 82 4 1 0 0 -1");

            Check.That(packet.EntityType).Is(EntityType.DROP);
            Check.That(packet.Vnum).Is(503);
            Check.That(packet.EntityId).Is(4808544);
            Check.That(packet.PositionX).Is<short>(82);
            Check.That(packet.PositionY).Is<short>(4);
            Check.That(packet.DropSubPacket.Amount).Is(1);
        }

        [Fact]
        public void In_Packet_As_Npc()
        {
            InPacket packet = Deserialize<InPacket>("in 2 334 2206 96 57 2 87 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");

            Check.That(packet.EntityType).Is(EntityType.NPC);
            Check.That(packet.Vnum).Is(334);
            Check.That(packet.EntityId).Is(2206);
            Check.That(packet.PositionX).Is<short>(96);
            Check.That(packet.PositionY).Is<short>(57);
            Check.That(packet.Direction).Is<byte>(2);
            Check.That(packet.NpcSubPacket.HpPercentage).Is<byte>(87);
            Check.That(packet.NpcSubPacket.MpPercentage).Is<byte>(100);
        }

        [Fact]
        public void In_Packet_As_Pet()
        {
            InPacket packet = Deserialize<InPacket>("in 2 815 749159 75 64 2 100 100 0 0 3 1290807 1 0 -1 Ratufu^centurion 2 -1 0 0 0 0 0 0 0 0");

            Check.That(packet.EntityType).Is(EntityType.NPC);
            Check.That(packet.Vnum).Is(815);
            Check.That(packet.EntityId).Is(749159);
            Check.That(packet.PositionX).Is<short>(75);
            Check.That(packet.PositionY).Is<short>(64);
            Check.That(packet.Direction).Is<byte>(2);
            Check.That(packet.NpcSubPacket.HpPercentage).Is<byte>(100);
            Check.That(packet.NpcSubPacket.MpPercentage).Is<byte>(100);
            Check.That(packet.NpcSubPacket.Owner).Is(1290807);
            Check.That(packet.NpcSubPacket.Name).Is("Ratufu centurion");
        }

        [Fact]
        public void In_Packet_As_Player()
        {
            InPacket packet = Deserialize<InPacket>(
                "in 1 ~Orage~ - 652158 100 58 2 0 0 0 53 1 204.4949.4964.4955.8026.4130.8205.4179.-1.4443 100 100 0 28 4 2 0 41 0 20 87 87 2582 100ßlaze(Membre) 24 0 15 0 12 92 8 0|0|0 0 35 10 28 93733");

            Check.That(packet.EntityType).Is(EntityType.PLAYER);
            Check.That(packet.Name).Is("~Orage~");
            Check.That(packet.EntityId).Is(652158);
            Check.That(packet.PositionX).Is<short>(100);
            Check.That(packet.PositionY).Is<short>(58);
            Check.That(packet.Direction).Is<byte>(2);
            Check.That(packet.PlayerSubPacket.Class).Is(ClassType.SWORDSMAN);
            Check.That(packet.PlayerSubPacket.Gender).Is(GenderType.MALE);
        }

        [Fact]
        public void Out_Packet()
        {
            OutPacket packet = Deserialize<OutPacket>("out 1 128476");

            Check.That(packet.EntityType).Is(EntityType.PLAYER);
            Check.That(packet.EntityId).Is(128476);
        }
    }
}