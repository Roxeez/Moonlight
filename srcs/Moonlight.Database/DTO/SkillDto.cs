using Moonlight.Core.Enums.Game;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    public class SkillDto : IDto<int>
    {
        public string NameKey { get; set; }
        public short Range { get; set; }

        public short ZoneRange { get; set; }

        public int CastTime { get; set; }

        public int Cooldown { get; set; }

        public SkillType SkillType { get; set; }

        public int MpCost { get; set; }

        public int CastId { get; set; }

        public TargetType TargetType { get; set; }

        public HitType HitType { get; set; }
        public int Id { get; set; }
    }
}