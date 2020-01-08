using NtCore.Core;
using NtCore.Enums;

namespace NtCore.Game.Maps
{
    public sealed class Portal
    {
        /// <summary>
        /// Portal Id
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Position in map
        /// </summary>
        public Position Position { get; }
        
        /// <summary>
        /// Destination map id
        /// </summary>
        public int DestinationId { get; }
        
        /// <summary>
        /// Type of portal
        /// </summary>
        public PortalType Type { get; set; }

        public Portal(int id, Position position, int destinationId)
        {
            Id = id;
            Position = position;
            DestinationId = destinationId;
        }
    }
}