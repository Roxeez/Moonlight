using Moonlight.Core;
using Moonlight.Core.Enums.Game;

namespace Moonlight.Game.Maps
{
    public class Portal
    {
        internal Portal(int id, Position position, int destinationId)
        {
            Id = id;
            Position = position;
            DestinationId = destinationId;
        }

        /// <summary>
        ///     Portal Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        ///     Position in map
        /// </summary>
        public Position Position { get; }

        /// <summary>
        ///     Destination map id
        /// </summary>
        public int DestinationId { get; }

        /// <summary>
        ///     Type of portal
        /// </summary>
        public PortalType Type { get; internal set; }
    }
}