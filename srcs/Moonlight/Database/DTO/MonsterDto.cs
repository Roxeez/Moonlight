using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class MonsterDto : IDto<int>
    {
        public string NameKey { get; set; }
        public int Level { get; set; }
        public int Id { get; set; }
    }
}