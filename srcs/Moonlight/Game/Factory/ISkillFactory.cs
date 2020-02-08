using Moonlight.Game.Battle;

namespace Moonlight.Game.Factory
{
    public interface ISkillFactory
    {
        Skill CreateSkill(int vnum);
    }
}