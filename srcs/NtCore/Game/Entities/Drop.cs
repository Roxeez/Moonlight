using NtCore.API;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Drop : IDrop
    {
        public long Id { get; set; }
        public int Vnum { get; set; }
        public int Amount { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
    }
}