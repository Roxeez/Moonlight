using System;
using NFluent;
using NtCore.Registry;
using Xunit;

namespace NtCore.Tests.Registry
{
    public class RegistryTests
    {
        private readonly IRegistry _registry;
        
        public RegistryTests()
        {
            _registry = NtCoreAPI.Instance.Registry;
        }
        
        [Theory]
        [InlineData(1250, "zts1845e", 4, 600, 0, 1, 0)]
        [InlineData(1254, "zts1849e", 4, 60, 0, 0, 0)]
        public void Skill_Registry_Return_Correct_Value(int skillVnum, string nameKey, byte skillType, int cooldown, int mpCost, int targetingType, int castId)
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