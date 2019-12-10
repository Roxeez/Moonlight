using NtCore.Enums;
using NtCore.Registry;

namespace NtCore.Game.Battle.Impl
{
    public class Skill : ISkill
    {
        private readonly SkillInfo _skillInfo;
        
        public int Vnum { get; set; }
        public string Name { get; set; }
        public int MpCost => _skillInfo.MpCost;
        public SkillType SkillType => (SkillType)_skillInfo.SkillType;
        public int Cooldown => _skillInfo.Cooldown;
        public int CastId => _skillInfo.CastId;
        public TargetingType TargetingType => (TargetingType)_skillInfo.TargetingType;

        public Skill(SkillInfo skillInfo)
        {
            _skillInfo = skillInfo;
        }
        
        public bool Equals(ISkill other) => other != null && other.Vnum.Equals(Vnum);
    }
}