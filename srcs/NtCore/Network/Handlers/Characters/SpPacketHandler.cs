using NtCore.Clients;
using NtCore.Extensions;
using NtCore.Game.Entities.Impl;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class SpPacketHandler : PacketHandler<SpPacket>
    {
        public override void Handle(IClient client, SpPacket packet)
        {
            var character = client.Character.As<Character>();

            character.SpPointInfo.MaximumSpPoints = packet.MaximumPoints;
            character.SpPointInfo.MaximumAdditionalSpPoints = packet.MaximumAdditionalPoints;

            character.SpPointInfo.SpPoints = packet.Points;
            character.SpPointInfo.AdditionalSpPoints = packet.AdditionalPoints;
        }
    }
}