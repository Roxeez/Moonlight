using NtCore.Game.Battle;

namespace NtCore.Game.Factory
{
    public interface ISkillFactory
    {
        ISkill CreateSkill(int vnum);
    }
}