using NtCore.Game.Battle;

namespace NtCore.Factory
{
    public interface ISkillFactory
    {
        ISkill CreateSkill(int vnum);
    }
}