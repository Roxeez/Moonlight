namespace NtCore.Registry
{
    public interface IRegistry
    {
        SkillInfo GetSkillInfo(int skillVnum);
        ItemInfo GetItemInfo(int itemVnum);
        MonsterInfo GetMonsterInfo(int monsterVnum);
    }
}