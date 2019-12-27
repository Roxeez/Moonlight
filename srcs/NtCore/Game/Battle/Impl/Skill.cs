using NtCore.Registry;

namespace NtCore.Game.Battle.Impl
{
    public class Skill : ISkill
    {
        public Skill(SkillInfo skillInfo) => Info = skillInfo;

        public int Vnum { get; set; }
        public string Name { get; set; }
        public SkillInfo Info { get; }

        public bool Equals(ISkill other) => other != null && other.Vnum.Equals(Vnum);
    }
}