using NtCore.API.Client;
using NtCore.Extensions;
using NtCore.Network.Packets.Character;

namespace NtCore.Network.Handlers.Character
{
    public class SpPacketHandler : PacketHandler<SpPacket>
    {
        public override void Handle(IClient client, SpPacket packet)
        {
            var character = client.Character.AsModifiable();

            character.MaximumSpPoints = packet.MaximumPoints;
            character.MaximumAdditionalSpPoints = packet.MaximumAdditionalPoints;

            character.SpPoints = packet.Points;
            character.AdditionalSpPoints = packet.AdditionalPoints;
        }
    }
}