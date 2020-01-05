using NtCore.Core;
using NtCore.Enums;

namespace NtCore.Game.Maps
{
    public sealed class Portal
    {
        public int Id { get; }
        public Position Position { get; }
        public int DestinationId { get; }
        public PortalType Type { get; set; }

        public Portal(int id, Position position, int destinationId)
        {
            Id = id;
            Position = position;
            DestinationId = destinationId;
        }
    }
}