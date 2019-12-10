using JetBrains.Annotations;

namespace NtCore.Registry
{
    public interface IRegistry
    {
        /// <summary>
        /// Get registry skill info of this skill
        /// </summary>
        /// <param name="skillVnum">Skill vnum</param>
        /// <returns>Skill info or null if none</returns>
        [CanBeNull]
        SkillInfo GetSkillInfo(int skillVnum);
        
        /// <summary>
        /// Get registry item info of this item
        /// </summary>
        /// <param name="itemVnum">Item vnum</param>
        /// <returns>Item info or null if none</returns>
        [CanBeNull]
        ItemInfo GetItemInfo(int itemVnum);
        
        /// <summary>
        /// Get registry monster info of this item
        /// </summary>
        /// <param name="monsterVnum">Monster vnum</param>
        /// <returns>Monster info or null if none</returns>
        [CanBeNull]
        MonsterInfo GetMonsterInfo(int monsterVnum);
    }
}