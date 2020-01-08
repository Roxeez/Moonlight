using System.Diagnostics;
using Moq;
using NFluent;
using NtCore.Clients;
using NtCore.Core;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Entities;
using NtCore.Game.Items;
using NtCore.Game.Maps;
using NtCore.Network;
using NtCore.Registry;
using NtCore.Tests.Utility;
using Xunit;

namespace NtCore.Tests.PacketHandling
{
    public class CharacterPacketTests
    {
        public CharacterPacketTests()
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

        public const int CharacterId = 99999;

        private readonly IClient _client;

        [Theory]
        [InlineData("ski 240 241", 240, 241)]
        [InlineData("ski 248 240", 248, 240)]
        public void Ski_Packet_Set_Character_Skills(string packet, int firstSkill, int secondSkill)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Skills).HasElementThatMatches(x => x.Vnum == firstSkill);
            Check.That(_client.Character.Skills).HasElementThatMatches(x => x.Vnum == secondSkill);
        }

        [Theory]
        [InlineData("stat 2000 3000 1000 1500", 2000, 3000, 1000, 1500)]
        [InlineData("stat 1 2 3 4", 1, 2, 3, 4)]
        public void Stat_Packet_Update_Character_Stats(string packet, int hp, int maxHp, int mp, int maxMp)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Hp).IsEqualTo(hp);
            Check.That(_client.Character.Mp).IsEqualTo(mp);
            Check.That(_client.Character.MaxHp).IsEqualTo(maxHp);
            Check.That(_client.Character.MaxMp).IsEqualTo(maxMp);
            Check.That(_client.Character.HpPercentage).IsNotZero();
            Check.That(_client.Character.MpPercentage).IsNotZero();
            
            Trace.WriteLine(_client.Character.HpPercentage);
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
        public void Sp_Packet_Update_Character_Sp_Points(string packet, int additionalPoints,
            int maximumAdditionalPoints, int points, int maximumPoints)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.SpPointInfo.AdditionalSpPoints).IsEqualTo(additionalPoints);
            Check.That(_client.Character.SpPointInfo.MaximumAdditionalSpPoints).IsEqualTo(maximumAdditionalPoints);
            Check.That(_client.Character.SpPointInfo.SpPoints).IsEqualTo(points);
            Check.That(_client.Character.SpPointInfo.MaximumSpPoints).IsEqualTo(maximumPoints);
        }

        [Theory]
        [InlineData("cond 1 47 0 0 10", EntityType.PLAYER, 47, 10)]
        [InlineData("cond 2 2053 0 1 14", EntityType.NPC, 2053, 14)]
        [InlineData("cond 3 1874 1 0 8", EntityType.MONSTER, 1874, 8)]
        public void Cond_Packet_Change_Entity_Speed(string packet, EntityType entityType, int entityId, int speed)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, entityId).Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            LivingEntity entity = _client.Character.Map.GetEntity<LivingEntity>(entityType, entityId);

            Check.That(entity).IsNotNull();
            Check.That(entity.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("walk 15 35 0 10", 15, 35, 10)]
        [InlineData("walk 128 11 0 6", 128, 11, 6)]
        public void Walk_Packet_Change_Character_Position_And_Speed(string packet, byte x, byte y, byte speed)
        {
            _client.SendPacket(packet);

            Check.That(_client.Character.Position).IsEqualTo(new Position(x, y));
            Check.That(_client.Character.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("mv 2 2053 18 10 21", EntityType.NPC, 2053, 18, 10, 21)]
        [InlineData("mv 3 874 87 3 14", EntityType.MONSTER, 874, 87, 3, 14)]
        public void Mv_Packet_Change_Entity_Position_And_Speed(string packet, EntityType entityType, int id, byte x, byte y, byte speed)
        {
            Map fakeMap = new MapBuilder().WithEntity(entityType, id).Create();

            fakeMap.AddEntity(_client.Character);

            _client.ReceivePacket(packet);

            LivingEntity entity = _client.Character.Map.GetEntity<LivingEntity>(entityType, id);

            Check.That(entity.Position).IsEqualTo(new Position(x, y));
            Check.That(entity.Speed).IsEqualTo(speed);
        }

        [Theory]
        [InlineData("c_info Roxeetest - -1 -1 - 1290125 0 1 0 9 0 1 0 0 0 0 0 0 0", "Roxeetest", 1290125, Gender.FEMALE, ClassType.ADVENTURER)]
        [InlineData("c_info Roxeez - -1 -1 - 999999 0 0 0 9 1 1 0 0 0 0 0 0 0", "Roxeez", 999999, Gender.MALE, ClassType.SWORDSMAN)]
        public void CInfo_Packet_Initialize_Character(string packet, string name, int id, Gender gender, ClassType classType)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Name).IsEqualTo(name);
            Check.That(_client.Character.Id).IsEqualTo(id);
            Check.That(_client.Character.Gender).IsEqualTo(gender);
            Check.That(_client.Character.Class).IsEqualTo(classType);
        }

        [Theory]
        [InlineData("lev 5 3840 3 1380 5460 3600 0 6 0 0 3 0", 5, 3)]
        [InlineData("lev 55 3840 28 1380 5460 3600 0 6 0 0 3 0", 55, 28)]
        public void Lev_Packet_Change_Character_Level(string packet, int level, int jobLevel)
        {
            _client.ReceivePacket(packet);

            Check.That(_client.Character.Level).IsEqualTo(level);
            Check.That(_client.Character.JobLevel).IsEqualTo(jobLevel);
        }

        [Fact]
        public void At_Packet_Change_Position()
        {
            Map map = new MapBuilder().WithId(1).Create();
            Character character = _client.Character;

            map.AddEntity(character);

            _client.ReceivePacket($"at {CharacterId} 1 5 8 2 0 80 6 -1");

            Check.That(character.Position).IsEqualTo(new Position(5, 8));
        }

        [Fact]
        public void Equip_Packet_Initialize_Character_Equipment()
        {
            _client.ReceivePacket("equip 0 0 0.124.1.0 1.148.7.10 10.87.0.0");

            Character character = _client.Character;

            Check.That(character.Equipment).IsNotNull();
            Check.That(character.Equipment.MainWeapon).IsNotNull();
            Check.That(character.Equipment.Armor).IsNotNull();
            Check.That(character.Equipment.Fairy).IsNotNull();
            Check.That(character.Equipment.MainWeapon.Vnum).IsEqualTo(124);
            Check.That(character.Equipment.MainWeapon.Rarity).IsEqualTo(1);
            Check.That(character.Equipment.MainWeapon.Upgrade).IsEqualTo(0);
            Check.That(character.Equipment.Armor.Vnum).IsEqualTo(148);
            Check.That(character.Equipment.Armor.Rarity).IsEqualTo(7);
            Check.That(character.Equipment.Armor.Upgrade).IsEqualTo(10);
        }

        [Fact]
        public void Pairy_Packet_Set_Fairy_Element_And_Power()
        {
            Character character = _client.Character;
            character.Equipment.Fairy = new Fairy(1, "", new ItemInfo());
            
            _client.ReceivePacket($"pairy 1 {CharacterId} 0 4 2 41 0");

            Check.That(character.Equipment.Fairy).IsNotNull();
            Check.That(character.Equipment.Fairy.Element).IsEqualTo(Element.WATER);
            Check.That(character.Equipment.Fairy.Power).IsEqualTo(41);
        }

        [Fact]
        public void PInit_Set_Character_Party()
        {
            Map map = new MapBuilder().WithId(1).WithNpcs(55, 66).WithPlayers(77).Create();
            Character character = _client.Character;

            map.AddEntity(character);

            _client.ReceivePacket($"pinit 4 2|55|0|6|Léona|1|822|1 2|66|1|41|Fenrir|1|843|0 1|{CharacterId}|2|50|Roxeez|1|0|2|0|0 1|77|3|3|Testastos|1|1|0|0|0");

            Check.That(character.Party).IsNotNull();
            Check.That(character.Party.Members).CountIs(4);
            Check.That(character.Party.Owner).IsEqualTo(character);
        }

        [Fact]
        public void At_Packet_Set_Map()
        {
            Character character = _client.Character;

            _client.ReceivePacket($"at {CharacterId} 1 50 50 1 0 0 0 0");

            Check.That(character.Map.Id).IsEqualTo(1);
            Check.That(character.Position).IsEqualTo(new Position(50, 50));
        }
    }
}