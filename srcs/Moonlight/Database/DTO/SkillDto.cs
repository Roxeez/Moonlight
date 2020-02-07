using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class SkillDto : IDto<int>
    {
        public string NameKey { get; set; }
        public int Id { get; set; }
    }
}