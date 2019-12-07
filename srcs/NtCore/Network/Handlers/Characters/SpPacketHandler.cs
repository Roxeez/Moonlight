using NtCore.API.Clients;
using NtCore.API.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class SpPacketHandler : PacketHandler<SpPacket>
    {
        public override void Handle(IClient client, SpPacket packet)
        {
            var character = client.Character.As<Character>();

            character.MaximumSpPoints = packet.MaximumPoints;
            character.MaximumAdditionalSpPoints = packet.MaximumAdditionalPoints;

            character.SpPoints = packet.Points;
            character.AdditionalSpPoints = packet.AdditionalPoints;
        }
    }
}