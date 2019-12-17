using NtCore.Clients;
using NtCore.Enums;
using NtCore.Extensions;
using NtCore.Game.Items.Impl;
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

            if (packet.EntityId != client.Character.Id)
            {
                return;
            }
            
            if (client.Character.Equipment.Fairy == null)
            {
                return;
            }

            var fairy = client.Character.Equipment.Fairy.As<Fairy>();

            fairy.Element = packet.Element;
            fairy.Power = packet.Power;
        }
    }
}