using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Game.Battle;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories.Items;
using Moonlight.Game.Maps;
using Moonlight.Tests.Extensions;
using Moonlight.Translation;
using NFluent;
using Xunit;

namespace Moonlight.Tests
{
    public class FactoryTests
    {
        private readonly IItemFactory _itemFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly IMapFactory _mapFactory;
        private readonly ISkillFactory _skillFactory;
        private readonly ILanguageService _languageService;

        public FactoryTests()
        {
            var moonlight = new MoonlightAPI(new AppConfig
            {
                Database = "../../database.db"
            });

            _itemFactory = moonlight.Services.GetService<IItemFactory>();
            _entityFactory = moonlight.Services.GetService<IEntityFactory>();
            _mapFactory = moonlight.Services.GetService<IMapFactory>();
            _skillFactory = moonlight.Services.GetService<ISkillFactory>();
            _languageService = moonlight.Services.GetService<ILanguageService>();
        }

        [Fact]
        public void Skill_Factory_Create_Correct_Skill()
        {
            Skill skill = _skillFactory.CreateSkill(243);

            Check.That(skill).IsNotNull();
            Check.That(skill.Id).Is(243);
            Check.That(skill.Name).Is("Concentrated Shot");
            Check.That(skill.Range).Is<short>(10);
            Check.That(skill.CastTime).Is(2);
            Check.That(skill.Cooldown).Is(150);
            Check.That(skill.MpCost).Is(0);
            Check.That(skill.TargetType).Is(TargetType.TARGET);
            Check.That(skill.HitType).Is(HitType.TARGET_ONLY);
            Check.That(skill.SkillType).Is(SkillType.PLAYER);
        }

        [Fact]
        public void Entity_Factory_Create_Correct_Monster()
        {
            Monster monster = _entityFactory.CreateMonster(1, 6);

            Check.That(monster).IsNotNull();
            Check.That(monster.Id).Is(1);
            Check.That(monster.Vnum).Is(6);
            Check.That(monster.Name).Is("Happy Oto-Fox");
            Check.That(monster.Level).Is(26);
        }

        [Fact]
        public void Entity_Factory_Create_Correct_Npc()
        {
            Npc npc = _entityFactory.CreateNpc(1, 164, string.Empty);

            Check.That(npc).IsNotNull();
            Check.That(npc.Id).Is(1);
            Check.That(npc.Vnum).Is(164);
            Check.That(npc.Name).Is("Slade");
            Check.That(npc.Level).Is(72);
        }

        [Fact]
        public void Entity_Factory_Create_Correct_Player()
        {
            Player player = _entityFactory.CreatePlayer(999, "Mermoud");

            Check.That(player).IsNotNull();
            Check.That(player.Id).Is(999);
            Check.That(player.Name).Is("Mermoud");
        }

        [Fact]
        public void Entity_Factory_Create_Correct_Ground_Item()
        {
            GroundItem groundItem = _entityFactory.CreateGroundItem(1, 759, 10);

            Check.That(groundItem).IsNotNull();
            Check.That(groundItem.Id).Is(1);
            Check.That(groundItem.Amount).Is(10);
            Check.That(groundItem.Name).Is("Archmage Wand");
        }

        [Fact]
        public void Item_Factory_Create_Correct_Item()
        {
            Item item = _itemFactory.CreateItem(759);

            Check.That(item).IsNotNull();
            Check.That(item.Vnum).Is(759);
            Check.That(item.Name).Is("Archmage Wand");
            Check.That(item.BagType).Is(BagType.EQUIPMENT);
            Check.That(item.Type).Is(0);
            Check.That(item.SubType).Is(9);
            Check.That(item.Data).CountIs(20);
            Check.That(item.Data).HasElementAt(0).WhichIs<short>(73);
            Check.That(item.Data).HasElementAt(1).WhichIs<short>(318);
            Check.That(item.Data).HasElementAt(2).WhichIs<short>(366);
            Check.That(item.Data).HasElementAt(3).WhichIs<short>(70);
        }

        [Fact]
        public void Map_Factory_Create_Correct_Map()
        {
            Map map = _mapFactory.CreateMap(1);

            Check.That(map).IsNotNull();
            Check.That(map.Id).Is(1);
            Check.That(map.Name).Is("NosVille");
            Check.That(map.Height).Is<short>(180);
            Check.That(map.Width).Is<short>(160);
        }

        [Theory]
        [InlineData("zts1e", "Rest")]
        [InlineData("zts2e", "Pick Up")]
        [InlineData("zts3e", "Open Shop")]
        public void Language_Service_Return_Correct_Skill_Name(string key, string result)
        {
            string skillName = _languageService.GetTranslation(RootKey.SKILL, key);
            
            Check.That(skillName).Is(result);
        }

        [Theory]
        [InlineData("zts1e", "Baby Fox")]
        [InlineData("zts2e", "Fox")]
        [InlineData("zts3e", "Big Fox")]
        public void Language_Service_Return_Correct_Monster_Name(string key, string result)
        {
            string skillName = _languageService.GetTranslation(RootKey.MONSTER, key);
            
            Check.That(skillName).Is(result);
        }
        
        [Theory]
        [InlineData("zts3e", "Wooden Stick")]
        [InlineData("zts7e", "Wooden Sword")]
        [InlineData("zts9e", "Wooden Hammer")]
        public void Language_Service_Return_Correct_Item_Name(string key, string result)
        {
            string skillName = _languageService.GetTranslation(RootKey.ITEM, key);
            
            Check.That(skillName).Is(result);
        }
        
        [Theory]
        [InlineData("zts1e", "NosVille")]
        [InlineData("zts2e", "NosVille Meadows")]
        [InlineData("zts3e", "Mine Plains")]
        public void Language_Service_Return_Correct_Map_Name(string key, string result)
        {
            string skillName = _languageService.GetTranslation(RootKey.MAP, key);
            
            Check.That(skillName).Is(result);
        }
    }
}