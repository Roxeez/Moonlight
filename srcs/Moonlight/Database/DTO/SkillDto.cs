using Moonlight.Core.Enums;
using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class SkillDto : IDto<int>
    {
        public int Id { get; set; }
        public string NameKey { get; set; }
        public short Range { get; set; }
        
        public int Cooldown { get; set; }
        
        public SkillType SkillType { get; set; }
        
        public int MpCost { get; set; }
        
        public int CastId { get; set; }
        
        public TargetType TargetType { get; set; }
    }
}