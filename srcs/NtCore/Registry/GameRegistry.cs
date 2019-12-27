using System.Collections.Generic;
using NtCore.Extensions;
using NtCore.Resources;

namespace NtCore.Registry
{
    public class GameRegistry : IRegistry
    {
        private readonly ResourceManager _resourceManager;
        private Dictionary<int, ItemInfo> _itemInfos;
        private Dictionary<int, MapInfo> _mapInfos;
        private Dictionary<int, MonsterInfo> _monsterInfos;
        private Dictionary<int, SkillInfo> _skillInfos;

        public GameRegistry(ResourceManager resourceManager) => _resourceManager = resourceManager;

        public SkillInfo GetSkillInfo(int skillVnum) => _skillInfos.GetValueOrDefault(skillVnum);

        public ItemInfo GetItemInfo(int itemVnum) => _itemInfos.GetValueOrDefault(itemVnum);

        public MonsterInfo GetMonsterInfo(int monsterVnum) => _monsterInfos.GetValueOrDefault(monsterVnum);

        public MapInfo GetMapInfo(int mapId) => _mapInfos.GetValueOrDefault(mapId);

        public void Load()
        {
            _skillInfos = _resourceManager.Load<Dictionary<int, SkillInfo>>("Skill.json");
            _monsterInfos = _resourceManager.Load<Dictionary<int, MonsterInfo>>("monster.json");
            _itemInfos = _resourceManager.Load<Dictionary<int, ItemInfo>>("Item.json");
            _mapInfos = _resourceManager.Load<Dictionary<int, MapInfo>>("MapIDData.json");
        }
    }
}