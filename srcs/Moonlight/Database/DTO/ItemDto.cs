using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class ItemDto : IDto<int>
    {
        public string NameKey { get; set; }
        public int Id { get; set; }
    }
}