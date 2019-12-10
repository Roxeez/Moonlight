using NtCore.Enums;

namespace NtCore.Game.Battle.Impl
{
    public class Skill : ISkill
    {
        public int Vnum { get; set; }
        public string Name { get; set; }
        public int MpCost { get; set; }
        public SkillType SkillType { get; set; }
        public int Cooldown { get; set; }
        public bool Equals(ISkill other) => other != null && other.Vnum.Equals(Vnum);
    }
}