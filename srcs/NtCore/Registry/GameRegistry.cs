using System.Collections.Generic;
using JetBrains.Annotations;
using NtCore.Extensions;
using NtCore.Resources;

namespace NtCore.Registry
{
    public class GameRegistry : IRegistry
    {
        private Dictionary<int, SkillInfo> _skillInfos;
        private Dictionary<int, MonsterInfo> _monsterInfos;
        private Dictionary<int, ItemInfo> _itemInfos;
        private Dictionary<int, MapInfo> _mapInfos;


        public SkillInfo GetSkillInfo(int skillVnum)
        {
            return _skillInfos.GetValueOrDefault(skillVnum);
        }

        public ItemInfo GetItemInfo(int itemVnum)
        {
            return _itemInfos.GetValueOrDefault(itemVnum);
        }

        public MonsterInfo GetMonsterInfo(int monsterVnum)
        {
            return _monsterInfos.GetValueOrDefault(monsterVnum);
        }

        public MapInfo GetMapInfo(int mapId) => null;

        public void Load()
        {
            _skillInfos = Resource.LoadJson<Dictionary<int, SkillInfo>>("Skill.json");
            _monsterInfos = Resource.LoadJson<Dictionary<int, MonsterInfo>>("monster.json");
            _itemInfos = Resource.LoadJson<Dictionary<int, ItemInfo>>("Item.json");
            _mapInfos = Resource.LoadJson<Dictionary<int, MapInfo>>("MapIDData.json");
        }
    }
}