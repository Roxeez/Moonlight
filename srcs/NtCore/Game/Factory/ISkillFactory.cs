using NtCore.Game.Battle;

namespace NtCore.Game.Factory
{
    public interface ISkillFactory
    {
        Skill CreateSkill(int vnum);
    }
}