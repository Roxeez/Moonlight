using NtCore.Clients;
using NtCore.Core;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class GpPacketHandler : PacketHandler<GpPacket>
    {
        public override void Handle(IClient client, GpPacket packet)
        {
            Map map = client.Character.Map;

            Portal portal = map.GetPortal(packet.PortalId);
            if (portal == null)
            {
                portal = new Portal(packet.PortalId, new Position(packet.SourceX, packet.SourceY), packet.DestinationId);
                map.AddPortal(portal);
            }

            portal.Type = packet.PortalType;
        }
    }
}