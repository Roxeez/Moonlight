using System;
using NFluent;
using NtCore.Enums;
using NtCore.Registry;
using Xunit;

namespace NtCore.Tests.Registry
{
    public class RegistryTests
    {
        private readonly IRegistry _registry;
        
        public RegistryTests()
        {
            _registry = NtCoreAPI.GetRegistry();
        }
        
        [Theory]
        [InlineData(1250, "zts1845e", SkillType.MONSTER, 600, 0, TargetingType.SELF, 0)]
        [InlineData(1254, "zts1849e", SkillType.MONSTER, 60, 0, TargetingType.TARGET, 0)]
        public void Skill_Registry_Return_Correct_Value(int skillVnum, string nameKey, SkillType skillType, int cooldown, int mpCost, TargetingType targetingType, int castId)
        {
            SkillInfo skillInfo = _registry.GetSkillInfo(skillVnum);

            if (skillInfo == null)
            {
                throw new InvalidOperationException();
            }
            
            Check.That(skillInfo.NameKey).IsEqualTo(nameKey);
            Check.That(skillInfo.SkillType).IsEqualTo(skillType);
            Check.That(skillInfo.Cooldown).IsEqualTo(cooldown);
            Check.That(skillInfo.MpCost).IsEqualTo(mpCost);
            Check.That(skillInfo.TargetingType).IsEqualTo(targetingType);
            Check.That(skillInfo.CastId).IsEqualTo(castId);
        }

        [Theory]
        [InlineData(1250, "zts1496e", 84)]
        [InlineData(1251, "zts1497e", 82)]
        public void Monster_Registry_Return_Correct_Value(int monsterVnum, string nameKey, int level)
        {
            MonsterInfo monsterInfo = _registry.GetMonsterInfo(monsterVnum);
            
            if (monsterInfo == null)
            {
                throw new InvalidOperationException();
            }
            
            Check.That(monsterInfo.NameKey).IsEqualTo(nameKey);
            Check.That(monsterInfo.Level).IsEqualTo(level);
        }

        [Theory]
        [InlineData(1250, "zts1630e", 0, 0)]
        [InlineData(320, "zts511e", 0, 0)]
        public void Item_Registry_Return_Correct_Value(int itemVnum, string nameKey, int inventoryTab, int equipmentSlot)
        {
            ItemInfo itemInfo = _registry.GetItemInfo(itemVnum);

            if (itemInfo == null)
            {
                throw new InvalidOperationException();
            }
            
            Check.That(itemInfo.NameKey).IsEqualTo(nameKey);
            Check.That(itemInfo.InventoryTab).IsEqualTo(inventoryTab);
            Check.That(itemInfo.EquipmentSlot).IsEqualTo(equipmentSlot);
        }
    }
}