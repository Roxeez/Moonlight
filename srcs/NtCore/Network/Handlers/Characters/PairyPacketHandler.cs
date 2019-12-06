using NtCore.API.Client;
using NtCore.API.Enums;
using NtCore.API.Extensions;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class PairyPacketHandler : PacketHandler<PairyPacket>
    {
        public override void Handle(IClient client, PairyPacket packet)
        {
            if (packet.EntityType != EntityType.PLAYER)
            {
                return;
            }

            var character = client.Character.As<Character>();
            if (character.Id != packet.EntityId)
            {
                return;
            }
        }
    }
}