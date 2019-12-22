using System.Collections.Generic;
using JetBrains.Annotations;
using NtCore.Extensions;

namespace NtCore.Registry
{
    public class GameRegistry : IRegistry
    {
        private readonly Dictionary<int, SkillInfo> _skillInfos;
        private readonly Dictionary<int, MonsterInfo> _monsterInfos;
        private readonly Dictionary<int, ItemInfo> _itemInfos;
        private readonly Dictionary<int, MapInfo> _mapInfos;

        public GameRegistry(Dictionary<int, SkillInfo> skillInfos, Dictionary<int, MonsterInfo> monsterInfos, Dictionary<int, ItemInfo> itemInfos)
        {
            _skillInfos = skillInfos;
            _monsterInfos = monsterInfos;
            _itemInfos = itemInfos;
        }

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
    }
}