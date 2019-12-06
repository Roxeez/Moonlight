using NtCore.API;
using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Game.Entities;
using NtCore.API.Game.Maps;

namespace NtCore.Game.Entities
{
    public class Player : LivingEntity, IPlayer
    {
        public string Name { get; set; }
        public int Reputation { get; set; }
        public int Dignity { get; set; }
        public ClassType Class { get; set; }
        public Gender Gender { get; set; }
        public byte Level { get; set; }
        public Faction Faction { get; set; }

        public Player()
        {
            EntityType = EntityType.PLAYER;
        }
    }
}