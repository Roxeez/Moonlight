using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class MlInfoBrPacketHandler : PacketHandler<MlInfoBrPacket>
    {
        public override void Handle(IClient client, MlInfoBrPacket packet)
        {
            var miniland = client.Character.Map.As<Miniland>();

            if (miniland == null)
            {
                return;
            }

            miniland.Owner = packet.Owner;
            miniland.Visitor = packet.Visitor;
            miniland.TotalVisitor = packet.TotalVisitor;
            miniland.Message = packet.Message;
        }
    }
}