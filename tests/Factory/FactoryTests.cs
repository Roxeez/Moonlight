using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Database;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Database.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Factory.Impl;
using Moonlight.Game.Maps;
using Moonlight.Tests.Extensions;
using Moonlight.Translation;
using NFluent;
using Xunit;
using Map = Moonlight.Database.Entities.Map;

namespace Moonlight.Tests.Factory
{
    public class FactoryTests
    {
        public FactoryTests()
        {
            var contextFactory = new SqliteContextFactory(new AppConfig
            {
                Database = "../../database.db"
            });

            var itemRepository = new Repository<Item, ItemDto, MoonlightContext>(contextFactory, new MapsterMapper<Item, ItemDto>());
            var monsterRepository = new Repository<Monster, MonsterDto, MoonlightContext>(contextFactory, new MapsterMapper<Monster, MonsterDto>());
            var mapRepository = new Repository<Map, MapDto, MoonlightContext>(contextFactory, new MapsterMapper<Map, MapDto>());
            var translationRepository =
                new StringRepository<Database.Entities.Translation, TranslationDto, MoonlightContext>(contextFactory, new MapsterMapper<Database.Entities.Translation, TranslationDto>());

            _languageService = new LanguageService(translationRepository)
            {
                Language = Language.EN
            };

            _itemFactory = new ItemFactory(itemRepository, _languageService);
            _entityFactory = new EntityFactory(_languageService, monsterRepository, _itemFactory);
            _mapFactory = new MapFactory(_languageService, mapRepository);
            _minilandObjectFactory = new MinilandObjectFactory(_itemFactory);
        }

        private readonly IItemFactory _itemFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly IMapFactory _mapFactory;
        private readonly IMinilandObjectFactory _minilandObjectFactory;
        private readonly ILanguageService _languageService;

        [Fact]
        public void Entity_Factory_Create_Correct_Entity()
        {
            Game.Entities.Monster monster = _entityFactory.CreateMonster(1, 125);

            Check.That(monster).IsNotNull();
            Check.That(monster.Id).IsEqualTo(1);
            Check.That(monster.Vnum).IsEqualTo(125);
        }

        [Fact]
        public void Item_Factory_Create_Correct_Item()
        {
            Game.Inventories.Items.Item item = _itemFactory.CreateItem(3125);

            Check.That(item).IsNotNull();
            Check.That(item.Vnum).IsEqualTo(3125);
        }

        [Fact]
        public void Language_Service_Return_Correct_Value()
        {
            string value = _languageService.GetTranslation(RootKey.SKILL, "zts174e");

            Check.That(value).Is("Rain of Arrows");
        }

        [Fact]
        public void Map_Factory_Create_Correct_Map()
        {
            Game.Maps.Map map = _mapFactory.CreateMap(1);

            Check.That(map.Id).IsEqualTo(1);
            Check.That(map.Name).IsEqualTo("NosVille");
        }

        [Fact]
        public void Miniland_Object_Factory_Create_Correct_Object()
        {
            MinilandObject minilandObject = _minilandObjectFactory.CreateMinilandObject(3125, 1, new Position(5, 5));

            Check.That(minilandObject.Item.Vnum).IsEqualTo(3125);
            Check.That(minilandObject.Slot).IsEqualTo(1);
            Check.That(minilandObject.Position).IsEqualTo(new Position(5, 5));
        }
    }
}