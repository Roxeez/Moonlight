using Moonlight.Database.DAL;

namespace Moonlight.Database.Dto
{
    internal class TranslationDto : IDto<string>
    {
        public string Value { get; set; }
        public string Id { get; set; }
    }
}