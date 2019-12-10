using NtCore.Enums;
using NtCore.Registry;

namespace NtCore.Game.Battle.Impl
{
    public class Skill : ISkill
    {
        public int Vnum { get; set; }
        public string Name { get; set; }
        public SkillInfo Info { get; }

        public Skill(SkillInfo skillInfo)
        {
            Info = skillInfo;
        }
        
        public bool Equals(ISkill other) => other != null && other.Vnum.Equals(Vnum);
    }
}