using Moonlight.Core.Enums.Game;

namespace Moonlight.Game.Entities
{
    /// <summary>
    ///     Represent a player
    /// </summary>
    public class Player : LivingEntity
    {
        internal Player(long id, string name) : base(id, name, EntityType.PLAYER)
        {
        }

        /// <summary>
        ///     Class of player
        /// </summary>
        public ClassType Class { get; internal set; }

        /// <summary>
        ///     Gender of player
        /// </summary>
        public GenderType Gender { get; internal set; }
    }
}